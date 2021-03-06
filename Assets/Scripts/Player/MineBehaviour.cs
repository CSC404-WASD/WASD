using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MineBehaviour : MonoBehaviour
{

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explodeClip;

    public float armTime;

    public bool isActive = false;
    public Material activateMaterial;

    public float deactivateTime = 30.0f;
    public float mineRange = 10;
    public Text armTimer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateMine(armTime));
        _audioSource = GameObject.FindWithTag("SFX Audio Source").GetComponent<AudioSource>();
    }

    //triggers on contact w/ enemy when active
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) {
            return;
        }
        if (MineColliderCheck(other))
        {
            Destroy(this.gameObject);
        }
    }

    //wait time seconds till mine is active
    IEnumerator ActivateMine(float time) {
        float timeLeft = time;
        while (timeLeft > 0)
        {
            armTimer.text = String.Format("{0:0.0}", timeLeft);
            //Debug.Log(time);
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        isActive = true;
        armTimer.enabled = false;
        //change mine colour when active
        GetComponent<Renderer>().material = activateMaterial;
        StartCoroutine(DeactivateMine(deactivateTime));

        // Check for stuff currently in range
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, mineRange);
        bool destroyed = false;
        foreach(Collider other in hitColliders) {
            destroyed = destroyed || MineColliderCheck(other);
        }
        if (destroyed)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DeactivateMine(float time) {
        yield return new WaitForSeconds(time);
        PlayExplodeSound();
        Destroy(this.gameObject);
    }

    public void PlayExplodeSound() {
        if (_audioSource != null) {
            _audioSource.clip = _explodeClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }
        // Shake camera
        var cam = GameObject.Find("Camera");
        cam.GetComponent<CameraController>().Shake(0.2f);
    }


    public bool MineColliderCheck(Collider other) // returns whether anything blew up.
    {
        //can also use tags compare tag to make it look neater, but i figure other combat also uses layers
        //so im doing this for now 6= enemies 7 = strong enemies
        if (other.gameObject.layer == 6 || other.gameObject.layer == 7)
        {
            var enemyAI = other.GetComponent<BaseEnemyAI>();
            if (enemyAI != null)
            {
                PlayExplodeSound();
                enemyAI.Die();
                return true;
            }
        }
        if (other.CompareTag("Mineable Wall"))
        {
            PlayExplodeSound();
            Destroy(other.gameObject);
            return true;
        }
        return false;
    }
}

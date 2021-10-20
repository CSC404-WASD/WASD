using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explodeClip;

    private bool isActive = false;
    public Material activateMaterial;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateMine(2.0f));
        _audioSource = GameObject.FindWithTag("SFX Audio Source").GetComponent<AudioSource>();
    }

    //triggers on contact w/ enemy when active
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) {
            return;
        }
        //can also use tags compare tag to make it look neater, but i figure other combat also uses layers
        //so im doing this for now 6= enemies 7 = strong enemies
        if (other.gameObject.layer == 6 || other.gameObject.layer == 7)
        {
            PlayExplodeSound();
            Destroy(this.gameObject);
            var enemyAI = other.GetComponent<BaseEnemyAI>();
            enemyAI.Die();
        }
    }

    //wait time seconds till mine is active
    IEnumerator ActivateMine(float time) {
        yield return new WaitForSeconds(time);
        isActive = true;
        //change mine colour when active
        GetComponent<Renderer>().material = activateMaterial;
        //make mine not solid when active
        GetComponent<CapsuleCollider>().isTrigger = true;
        StartCoroutine(DeactivateMine(30.0f));
    }

    IEnumerator DeactivateMine(float time) {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    public void PlayExplodeSound() {
        if (_audioSource != null) {
            _audioSource.clip = _explodeClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }
}

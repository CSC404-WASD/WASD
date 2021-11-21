using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemyAI : MonoBehaviour
{
    EnemyController eController;
    protected GameObject player;
    private LoopingTracksPlayer _musicPlayer;

    protected bool stunned = false;
    private float stunTime = 0f;
    public bool active = true;
    public Text alertText;

    private bool flash = false;
    private Vector3 _originalScale;

    private float _step_time = 0f;
    public float step_delay = 0.35f;
    
    // Start is called before the first frame update
    public void Start()
    {
        //can also move this logic to a constructor (doesn't work with current 
        // singleton) or perhaps not the AI file
        eController = EnemyController.instance;
        eController.addEnemy();
        player = GameObject.FindWithTag("Player");
        _originalScale = this.transform.lossyScale;
        _musicPlayer = GetComponent<LoopingTracksPlayer>();
        if (active) {
            StartMusic(false);
        }
        //normalMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            stunTime -= Time.deltaTime;
            if (stunTime <= 0)
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                stunned = false;
            }
        }

        if (_musicPlayer != null) {
            if (_step_time < Time.time && _musicPlayer.IsPlaying()) {
                _musicPlayer.PlayRandomStep();
                _step_time = Time.time + step_delay;
            }
        }
    }

    // stun for at least duration
    public void stunForDuration(float duration)
    {
        stunned = true;
        stunTime = duration;
    }

    //Start dying sequence
    public void Die() {
        // Update wall open triggers
        var parent = this.gameObject.GetComponentInParent<EnemyKillWallOpenTrigger>();
        if (parent != null) {
            parent.RemoveEnemy();
        }

        // Stop this enemy from moving
        active = false;
        this.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

        // Deactivate collider
        Collider collider = this.gameObject.GetComponent<Collider>();
        if (collider != null) {
            collider.enabled = false;
        }

        // Shake camera
        var cam = GameObject.Find("Camera");
        cam.GetComponent<CameraController>().Shake(0.1f);


        StartCoroutine(DeathFlash(0.1f));
        StartCoroutine(FinishDying(0.6f));
    }

    IEnumerator FinishDying(float time) {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
        eController.removeEnemy();
    }

    IEnumerator DeathFlash(float time) {
        while(true)
        {
            if (flash) 
            {
                this.transform.localScale = new Vector3(0,0,0);
            }
            else
            {
                this.transform.localScale = _originalScale;
            }
            flash = !flash;
            yield return new WaitForSeconds(time);
        }
    }

    public void ActivateEnemy(bool param) {
        active = param;
        if (alertText != null)
        {
            alertText.enabled = true;
            StartMusic(true);
            StartCoroutine(DisableAlert(3.0f));
        }

    }

    void StartMusic(bool aggro) {
        if (_musicPlayer != null) {
            if (aggro) {
                _musicPlayer.PlayAggroClip();
            }
            if (_musicPlayer.IsLoopingTrack()) {
                StartCoroutine(_musicPlayer.PlayLoop());
            } else {
                StartCoroutine(_musicPlayer.StartPlaying());
            }
        }
    }

    IEnumerator DisableAlert(float time) {
        yield return new WaitForSeconds(time);
        alertText.enabled = false;
    }
}

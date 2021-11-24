using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoopingTracksPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _clips;
    [SerializeField]
    private AudioClip _aggroClip;
    [SerializeField]
    private AudioClip _loopClip;
    private int _index = 0;
    private bool _playing = false;
    private System.Random _rnd;
    private AudioSource _source;
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _rnd = new System.Random();
    }

    public IEnumerator StartPlaying() {
        float len = 0f;
        if (_aggroClip != null) {
            len = _aggroClip.length;
        }
        yield return new WaitForSeconds(len);
        _playing = true;
    }

    //function for playing one of the _clips steps at random
    public void PlayRandomStep() {
        if (_source == null) {
            return;
        }
        int clip_num = _rnd.Next(0,_clips.Length);
        _source.loop = false;
        _source.PlayOneShot(_clips[clip_num], 0.33f);
    }

    //if the enemy has a looping walk track play this one, start after aggro clip
    public IEnumerator PlayLoop() {
        float len = 0f;
        if (_aggroClip != null) {
            len = _aggroClip.length;
        }
        yield return new WaitForSeconds(len);
        _source.clip = _loopClip;
        _source.loop = true;
        _source.Play();
    }

    public void PlayAggroClip() {
        if (_source == null || _aggroClip == null){
            return;
        }

        _source.loop = false;
        _source.PlayOneShot(_aggroClip, 1.0f);
    }

    public bool IsPlaying() {
        return _playing;
    }

    public bool IsLoopingTrack() {
        return _loopClip != null;
    }
    public bool IsRandomTrack() {
        return _clips.Length > 0;
    }
}

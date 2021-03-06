using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    //turn this class into singleton?
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _upClip;
    [SerializeField]
    private AudioClip _downClip;
    [SerializeField]
    private AudioClip _leftClip;
    [SerializeField]
    private AudioClip _rightClip;
    [SerializeField]
    private AudioClip _deathClip;
    [SerializeField]
    private AudioClip[] _leftChargedClip;
    [SerializeField]
    private AudioClip[] _rightChargedClip;
    [SerializeField]
    private AudioClip[] _downChargedClip;
    [SerializeField]
    private AudioClip[] _upChargedClip;
    [SerializeField]
    private AudioClip _fartClip;
    [SerializeField]
    private AudioClip _hitAttackClip;

    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
    }

    //could also use one method and clip as the arg for all
    public void PlayUpSound() {
        if (_audioSource != null) {
            var clip = _upClip;
            _audioSource.loop = false;
            _audioSource.PlayOneShot(clip, 0.8f);
        }
    }

    public void PlayDownSound() {
        if (_audioSource != null) {
            var clip = _downClip;
            _audioSource.loop = false;
            _audioSource.PlayOneShot(clip, 0.7f);
        }
    }

    public void PlayLeftSound() {
        if (_audioSource != null) {
            var clip = _leftClip;
            _audioSource.loop = false;
            _audioSource.PlayOneShot(clip, 0.8f);
        }
    }

    public void PlayRightSound() {
        if (_audioSource != null) {
            _audioSource.clip = _rightClip;
            _audioSource.loop = false;
            //this one allows multiple clips to be played from same audio source, not sure if we'll need
            _audioSource.PlayOneShot(_audioSource.clip, 0.5f);
        }
    }

    public void PlayDeathSound() {
        if (_audioSource != null) {
            _audioSource.clip = _deathClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }

    public void PlayChargedSound(string direction, int num)
    {
        if (_audioSource == null)
        {
            return;
        }

        _audioSource.loop = false;

        var clip = _upChargedClip[num];
        switch (direction)
        {
            case "up":
                clip = _upChargedClip[num];
                break;
            case "down":
                clip = _downChargedClip[num];
                break;
            case "left":
                clip = _leftChargedClip[num];
                break;
            case "right":
                clip = _rightChargedClip[num];
                break;
        }
        
        _audioSource.PlayOneShot(clip, 0.5f);
    }

    public void PlayFartSound()
    {
        if (_audioSource == null)
        {
            return;
        }

        _audioSource.loop = false;
        _audioSource.PlayOneShot(_fartClip, 0.7f);
    }

    public void PlayAttackHitSound()
    {
        if (_audioSource == null)
        {
            return;
        }

        _audioSource.loop = false;
        _audioSource.PlayOneShot(_hitAttackClip, 1.0f);
    }
}


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
    private AudioClip _leftChargedClip;
    [SerializeField]
    private AudioClip _rightChargedClip;
    [SerializeField]
    private AudioClip _downChargedClip;
    [SerializeField]
    private AudioClip _upChargedClip;
    [SerializeField]
    private AudioClip _fartClip;

    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
    }

    //could also use one method and clip as the arg for all
    public void PlayUpSound() {
        if (_audioSource != null) {
            _audioSource.clip = _upClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }

    public void PlayDownSound() {
        if (_audioSource != null) {
            _audioSource.clip = _downClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }

    public void PlayLeftSound() {
        if (_audioSource != null) {
            _audioSource.clip = _leftClip;
            _audioSource.loop = false;
            _audioSource.Play();
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

    public void PlayChargedSound(string direction)
    {
        if (_audioSource == null)
        {
            return;
        }

        _audioSource.loop = false;

        var clip = _upChargedClip;
        switch (direction)
        {
            case "up":
                clip = _upChargedClip;
                break;
            case "down":
                clip = _downChargedClip;
                break;
            case "left":
                clip = _leftChargedClip;
                break;
            case "right":
                clip = _rightChargedClip;
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
}

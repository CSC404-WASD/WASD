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
            _audioSource.Play();
        }
    }

    public void PlayDeathSound() {
        if (_audioSource != null) {
            _audioSource.clip = _deathClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }
}

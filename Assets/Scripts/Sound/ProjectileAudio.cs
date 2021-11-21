using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _shootClip;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootClip() {
        if (_audioSource != null) {
            _audioSource.clip = _shootClip;
            _audioSource.loop = false;
            //this one allows multiple clips to be played from same audio source, not sure if we'll need
            _audioSource.PlayOneShot(_audioSource.clip, 0.6f);
        }
    }
}

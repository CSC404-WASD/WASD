using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : SoundEffect
{
    // Start is called before the first frame update
    int counter = 0;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.loop = false;
        audiosource.clip = clips[counter];
        audiosource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if(!audiosource.isPlaying)
        {
            counter += 1;
            if(counter == clips.Length){
                counter = 0;
            }
            audiosource.clip = clips[counter];//Random.Range(0, clips.Length)];
            audiosource.Play();
            //audiosource.loop = true;
        }
    }
}

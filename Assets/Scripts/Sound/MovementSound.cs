using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : SoundEffect
{
    // Start is called before the first frame update
    void Start()
    {
        audiosource = FindObjectOfType<AudioSource>();
        audiosource.loop = true;
        audiosource.clip = clips[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0){
            if(!audiosource.isPlaying){
                /*counter += 1;
                if(counter == clips.Length){
                    counter = 0;
                }*/
                audiosource.clip = clips[0];//Random.Range(0, clips.Length)];
                audiosource.Play();
            }
        }else{
            audiosource.Stop();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    //public string path;
    public AudioClip[] clips;
    public AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Movement
        /*if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0){

        }*/

        //Background Music
        /*if(!audiosource.isPlaying)
        {
            audiosource.clip = clips[1];//Random.Range(0, clips.Length)];
            audiosource.Play();
            //audiosource.loop = true;
        }*/
    }
}

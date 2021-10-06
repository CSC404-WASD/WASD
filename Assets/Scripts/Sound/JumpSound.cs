using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    // Start is called before the first frame update
    int check = 0;
    private bool _onGround = false;
    void Start()
    {
        audiosource = FindObjectOfType<AudioSource>();
        audiosource.loop = true;
        audiosource.clip = clips[0];
    }

    // Update is called once per frame
    void Update()
    {

        _onGround = Physics.Raycast(_rigidbody.position, Vector3.down, out groundRaycastHit, maxGroundDistanceForJump);
        //Jump
        if(Input.GetButton("Jump")!=0 && check == 0 ){
            check = 1;
            audiosource.clip = clips[0];//Random.Range(0, clips.Length)];
            audiosource.Play();
        }
        
        elif(_onGround && check = 1){
            check = 0;
            audisource.clip = clips[1];
            audiosource.Play();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : SoundEffect
{
    // Start is called before the first frame update
    int check = 0;
    private bool _onGround = false;
    private Rigidbody _rigidbody;
    [Range(0, 100)] [SerializeField] private float maxGroundDistanceForJump = 5f;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        audiosource = GetComponent<AudioSource>();
        audiosource.loop = true;
        audiosource.clip = clips[0];
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit groundRaycastHit;
        _onGround = Physics.Raycast(_rigidbody.position, Vector3.down, out groundRaycastHit, maxGroundDistanceForJump);
        //Jump
        if(Input.GetButton("Jump") && check == 0 && _onGround){
            check = 1;
            audiosource.clip = clips[0];
            audiosource.Play();
        }
        else if( _onGround && check == 1){
            check = 0;
            audiosource.clip = clips[1];
            audiosource.Play();
        }

    }
}

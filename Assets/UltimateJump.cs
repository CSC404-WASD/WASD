using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateJump : MonoBehaviour
{
    float upSpeed = 10.0f;
    float downM = 2.0f;
    Vector3 jumpV = new Vector3(0.0f, 1.0f, 0.0f);

    int jumps = 0;
    //float lessdownM = 1f;
    Rigidbody jumperchad;

    // Start is called before the first frame update

    void Start()
    {
        jumperchad = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown ("Jump") && jumperchad.velocity.y == 0){

            jumperchad.velocity = jumpV*upSpeed;
            //jumperchad.AddForce(jumpV*upSpeed, ForceMode.Impulse);
            jumps += 1;
        }
        //jumperchad.velocity = new Vector3(0, 10, 0);

        /*if (jumperchad.velocity.y < 0){
            jumperchad.velocity += Vector3.up * Physics.gravity.y * downM *Time.deltaTime;
        }*/
    }
}

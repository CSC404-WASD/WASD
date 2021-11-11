using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 _cameraOffset;

    // How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) {
            return;
        }
        transform.position = player.transform.position + _cameraOffset;

        if (shakeDuration > 0)
		{
			transform.position = player.transform.position + _cameraOffset + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			transform.position = player.transform.position + _cameraOffset;
		}
    }
}

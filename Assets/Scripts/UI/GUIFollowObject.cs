using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIFollowObject : MonoBehaviour
{
    public Transform target;
    Camera cam;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var wantedPos = cam.WorldToScreenPoint(target.position + offset);
        this.transform.position = wantedPos; 
    }
}

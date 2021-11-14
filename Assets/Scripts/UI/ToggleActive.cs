using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleActive : MonoBehaviour
{
    public float toggleTime;
    private float _startTime;
    private bool _active;
    private bool _shouldFlash = true;

    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        _active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldFlash && Time.time >= _startTime + toggleTime) {
            _active = !_active;
            gameObject.GetComponent<Text>().enabled = _active;
            _startTime = Time.time;
        }
    }

    public void SetFlash(bool flash) {
        _shouldFlash = flash;
    }
}

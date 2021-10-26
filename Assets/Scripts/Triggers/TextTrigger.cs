using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    public Text text;
    public Image textBox;
    public string message;
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            text.text = message;
            text.enabled = true;
            textBox.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            text.enabled = false;
            textBox.enabled = false;
        }
    }
}
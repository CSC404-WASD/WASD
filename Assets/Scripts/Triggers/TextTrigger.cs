using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    private Text text;
    private Image textBox;
    public string message;
    public Animator anim;
    public void Awake()
    {
        text = GameObject.Find("MessageText").GetComponent<Text>();
        textBox = GameObject.Find("MessageBox").GetComponent<Image>();
    }
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            text.text = message;
            text.enabled = true;
            textBox.enabled = true;
            anim.SetTrigger("EnterSignTrigger");
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            text.enabled = false;
            textBox.enabled = false;
            anim.SetTrigger("ExitSignTrigger");
        }
    }
}
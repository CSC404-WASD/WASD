using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ImageRandomizer : MonoBehaviour
{
    GameController _gController;
    public Sprite[] images;
    private System.Random _rnd;
    public Boolean useRandom;
    public Boolean useProgressive;
    public int[] thresholds;
    public String[] progressiveTexts;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        var img = gameObject.GetComponent<Image>();
        if (img == null) {
            return;
        }
        if (useRandom) {
            _rnd = new System.Random();   
            img.sprite = images[_rnd.Next(0,images.Length)];
        } else if (useProgressive) {
            _gController = GameController.instance;
            if (_gController != null) {
                var deaths = _gController.getDeaths();
                int i = 0;
                text = GetComponentInChildren<Text>();
                foreach (var thresh in thresholds) {
                    if (deaths >= thresh) {
                        img.sprite = images[i];
                        if (text != null) {
                            text.text = progressiveTexts[i];
                        }
                    }
                    i += 1;
                    
                }
            }

        }
        
    }

}

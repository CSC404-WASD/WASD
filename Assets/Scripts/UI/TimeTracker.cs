using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class TimeTracker : MonoBehaviour
{
    private static TimeTracker _instance;
    public static TimeTracker instance {get {return _instance;}}
    //private LevelSwitchController lController;
    private float _timeElapsed = 0.0f;
    private float _totalTime = 0.0f;
    private bool _track = false;
    private List<float> times = new List<float>() {};
    public Text timeText;
    //private float[] _times;
    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start() {
        //FindTimeText();
    }

    void Update()
    {
        if (!_track) {
            return;
        }
        _timeElapsed += Time.deltaTime;
        //might be fine to have a lot of overhead.
        if (_timeElapsed > 1) {
            _totalTime += _timeElapsed;
            if (timeText == null) {
                FindTimeText();
            }
            timeText.text = ConvertTime(_totalTime);
            _timeElapsed = 0;
        }
    }
    void FindTimeText() {
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
    }

    String ConvertTime(float time) {
        if (time > 3600) {
            var hours = Convert.ToInt32(Math.Floor(time / 3600));
            var minutes2 = Convert.ToInt32(Math.Floor(time - (3600 * hours) / 60));
            var seconds2 = Convert.ToInt32(Math.Floor(time % 60));
            return String.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes2, seconds2);
        }
        var minutes = Convert.ToInt32(Math.Floor(time / 60));
        var seconds = Convert.ToInt32(Math.Floor(time % 60));
        return String.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    String GetTotalTime() {
        return ConvertTime(_totalTime);
    }

    public void SetTrack(bool track) {
        _track = track;
        if (!track) {
            _totalTime = 0;
        }
    }

    public void RecordTime() {
        times.Add(_totalTime);
    }

    public String GetLastTime(bool total) {
        if (total) {
            var sum = times.Sum();
            return ConvertTime(sum);
        }
        return ConvertTime(times[times.Count -1]);
    }

    public void EmptyTimes() {
        times.Clear();
    }

}

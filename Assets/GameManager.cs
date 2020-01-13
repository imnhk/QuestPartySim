using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    Timer timer;
    public GameObject timerText;

    void Start()
    {
        timer = new Timer(30, timerText);
        timer.Start();
    }

    void Update()
    {
        timer.Update();
    }
}

public class Timer
{
    float startTime;
    float leftTime;
    bool isActive;
    TextMeshProUGUI timerText;

    public Timer(float startTime, GameObject timerText)
    {
        this.startTime = startTime;
        this.timerText = timerText.GetComponent<TextMeshProUGUI>();
        Reset();
    }

    public bool IsOver{ get { return leftTime <= 0; } }

    public void Update()
    {
        if (isActive == false)
            return;

        leftTime -= Time.deltaTime;
        timerText.text = leftTime.ToString("F1");

        if (IsOver)
            isActive = false;
    }

    public void Reset()
    {
        leftTime = startTime;
        isActive = false;
        timerText.text = leftTime.ToString("F1");
    }

    public void Start()
    {
        if (IsOver)
            return;
        isActive = true;
    }

    public void Stop()
    {
        isActive = false;
    }
        
}

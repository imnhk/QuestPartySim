using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public Timer timer;

    public int score;

    private void Awake()
    {
        // Singleton pattern
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        timer = new Timer(30);
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

    public float LeftTime { get { return leftTime; } }
    public bool IsOver{ get { return leftTime <= 0; } }

    public Timer(float startTime)
    {
        this.startTime = startTime;
        Reset();
    }


    public void Update()
    {
        if (isActive == false)
            return;

        leftTime -= Time.deltaTime;

        if (IsOver)
            isActive = false;
    }

    public void Reset()
    {
        leftTime = startTime;
        isActive = false;
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

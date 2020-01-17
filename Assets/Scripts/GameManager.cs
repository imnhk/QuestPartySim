using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public Game game;
    public Animator doorAnimator;

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
        game = new Game();
    }

    void Update()
    {
        game.Update();
    }

    public void StartGame()
    {
        game.Start();

        // 문열어어어
        doorAnimator.SetTrigger("Open");
        UIManager.Instance.startButton.gameObject.SetActive(false);
    }

    // 디버그용
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

public class Game
{
    public const float TIME_LIMIT = 60;
    public const int DEFAULT_STR = 3;
    private Timer timer;

    public float LeftTime { get { return timer.LeftTime; } }
    public bool IsOn { get { return timer.IsOn; } }

    private int totalDamage;
    private int destroyCount;
    private int score;
    private int strength;
    public int TotalDamage
    {
        get { return totalDamage; }
        set
        {
            if (IsOn) return;
            else totalDamage = value;
        }
    }
    public int DestroyCount
    {
        get { return destroyCount; }
        set
        {
            if (IsOn) return;
            else destroyCount = value;
        }
    }
    public int Score
    {
        get { return score; }
        set
        {
            if (IsOn )score = value;
        }
    }
    public int Str
    {
        get { return strength; }
        set
        {
            if (IsOn) strength = value;
        }
    }

    public Game()
    {
        timer = new Timer(TIME_LIMIT);
        score = 0;
        totalDamage = 0;
        destroyCount = 0;
        strength = DEFAULT_STR;
    }

    public void Reset()
    {
        timer.Reset();
    }

    public void Start()
    {
        timer.Start();
    }

    public void Update()
    {
        if(IsOn)
            timer.Update();
    }

}

public class Timer
{
    float leftTime;
    bool isActive;

    public float startTime;
    public float LeftTime { get { return leftTime; } }
    public bool IsOver{ get { return leftTime <= 0; } }
    public bool IsOn { get { return isActive; } }

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

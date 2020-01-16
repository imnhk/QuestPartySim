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
    public Timer timer;

    public float LeftTime { get { return timer.LeftTime; } }
    public bool IsOver { get { return LeftTime <= 0; } }

    private int totalDamage;
    private int destroyCount;
    private int score;
    public int TotalDamage
    {
        get { return totalDamage; }
        set
        {
            if (IsOver) return;
            else totalDamage = value;
        }
    }
    public int DestroyCount
    {
        get { return destroyCount; }
        set
        {
            if (IsOver) return;
            else destroyCount = value;
        }
    }
    public int Score
    {
        get { return score; }
        set
        {
            if (IsOver) return;
            else score = value;
        }
    }

    public Game()
    {
        timer = new Timer(TIME_LIMIT);
    }

    public void Reset()
    {
        score = 0;
        timer.Reset();

        totalDamage = 0;
        destroyCount = 0;
    }

    public void Start()
    {
        timer.Start();
    }

    public void Update()
    {
        if(!timer.IsOver)
            timer.Update();
    }

    public void AddScore(int num)
    {
        if (IsOver)
            return;
        score += num;
    }

}

public class Timer
{
    float leftTime;
    bool isActive;

    public float startTime;
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

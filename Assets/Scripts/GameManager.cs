using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField]
    private float gameLength;

    private ElevatorAnimation elevator;
    private GameObject player;
    private OVRScreenFade screenFade;
    private AudioSource audioSrc;

    [SerializeField]
    private List<AudioClip> gameoverSounds;

    private Timer timer;
    public enum GAMEOVER { ARRESTED = 0, PASSED_OUT = 1, KICKED_OUT = 2, TIMEOUT = 3 }


    public float LeftTime { get { return timer.LeftTime; } }
    public bool IsOn { get { return timer.IsActive; } }

    private int score;
    private float alcohol;

    public void AddScore(int value)
    {
        if (!IsOn)
            return;

        UIManager.Instance.MakeDamagePopup(value);
        score += value;

    }
    public int Score
    {
        get { return score; }
    }
    public float Alcohol
    {
        get { return alcohol; }
        set
        {
            alcohol = value;
            if (alcohol < 0) alcohol = 0;
        }
    }

    private void Awake()
    {
        // Singleton pattern
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        audioSrc = GetComponent<AudioSource>();
        elevator = GameObject.Find("Elevator").GetComponent<ElevatorAnimation>();
        player = GameObject.Find("OVRPlayerController");
        screenFade = GameObject.Find("CenterEyeAnchor").GetComponent<OVRScreenFade>();
    }

    void Start()
    {
        timer = new Timer(gameLength);
        score = 0;
        alcohol = 0;
    }

    public void StartGameButton(float time)
    {
        if (timer.IsActive)
        {
            StartCoroutine(elevator.OpenDoorFor(3f));
        }
        else
        {
            StartCoroutine(StartGameIn(time));
        }
    }

    IEnumerator StartGameIn(float time)
    {
        UIManager.Instance.HideLeftHandPhone();
        elevator.audioSrc.Play();
        elevator.button.SetActive(false);

        yield return new WaitForSeconds(time);
        if (!timer.IsActive)
            timer.Start();

        elevator.audioSrc.Stop();
        elevator.button.SetActive(true);

        StartCoroutine(elevator.OpenDoorFor(3f));
        
    }

    void Update()
    {
        timer.Update();
        if(alcohol > 0.001f)
            alcohol -= 0.001f * Time.deltaTime;

        if (timer.IsOver)
        {
            GameOver(GAMEOVER.TIMEOUT);
            return;
        }

        if (!timer.IsActive)
            return;

        if(alcohol >= 0.3f)
        {
            GameOver(GAMEOVER.PASSED_OUT);
            return;
        }
    }

    public void GameOver(GAMEOVER type)
    {
        timer.Stop();
        Debug.Log("Game Over! " + type);
        GameStats.latestScore = score;
        GameStats.latestTime = timer.LeftTime;
        GameStats.gameoverType = (int)type;

        if (!timer.IsActive)
        {
            StartCoroutine(timeOut());
            return;
        }

        switch (type)
        {
            case GAMEOVER.PASSED_OUT:
                audioSrc.PlayOneShot(gameoverSounds[(int)GAMEOVER.PASSED_OUT]);
                StartCoroutine(passout());

                break;
            case GAMEOVER.KICKED_OUT:
                audioSrc.PlayOneShot(gameoverSounds[(int)GAMEOVER.KICKED_OUT]);
                StartCoroutine(kickedOut());
                break;
        }
    }

    public void DebugGameover(int type)
    {
        GameOver((GAMEOVER)type);
    }

    public IEnumerator kickedOut()
    {
        // 잠시 기다리고 카메라 Fadeout 

        yield return new WaitForSeconds(3f);
        screenFade.fadeTime = 3;
        screenFade.FadeOut();
        yield return new WaitForSeconds(3f);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public IEnumerator timeOut()
    {

        screenFade.fadeTime = 3;
        screenFade.FadeOut();
        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public IEnumerator passout()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<OVRPlayerController>().enabled = false;


        float elapsedTime = 0;

        while(elapsedTime < 1f)
        {
            player.transform.Rotate(Vector3.right, 90 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        screenFade.fadeTime = 2;
        screenFade.FadeOut();

        yield return new WaitForSeconds(2);
        // 게임오버 Scene으로
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}


public class Timer
{
    private float leftTime;
    private bool isActive;
    private float startTime;

    public float LeftTime { get { return leftTime; } }
    public bool IsOver{ get { return leftTime <= 0; } }
    public bool IsActive { get { return isActive; } }

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
        {
            Stop();
            leftTime = 0;
        }
    }

    public void Reset()
    {
        leftTime = startTime;
        Stop();
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

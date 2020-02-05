using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    static public UIManager Instance { get { return _instance; } }

    // Texts
    [Header("UI Text")]
    [SerializeField]
    private TextMeshProUGUI timerText;
    private AnimateText timerTextAnim;
    private int timerInt;
    //[SerializeField]
    //private TextMeshProUGUI strengthUI;
    [SerializeField]
    private List<TextMeshProUGUI> scoreTexts;
    private AnimateText scoreTextAnim;

    // Buttons
    [Header("Button")]
    public Button startButton;

    // Popups
    [Header("Score Popup")]
    [SerializeField]    GameObject scorePopup;
    [SerializeField]    int minSize = 10, maxSize = 20;
    [SerializeField]    Transform popupPos;
    [SerializeField]    Camera cam;

    // sfx
    [Header("Effects")]
    private OVRGrabber leftController;
    private AudioSource leftHandAudio;


    private Game currentGame;
    

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        currentGame = GameManager.Instance.game;
        timerText.text = currentGame.LeftTime.ToString("F2");
        timerTextAnim = timerText.gameObject.GetComponent<AnimateText>();
        timerInt = (int)currentGame.LeftTime;
        foreach (TextMeshProUGUI score in scoreTexts)
            score.text = currentGame.Score.ToString();
        scoreTextAnim = scoreTexts[0].gameObject.GetComponent<AnimateText>();

        leftController = GameObject.Find("DistanceGrabHandLeft").GetComponent<OVRGrabber>();
        leftHandAudio = GameObject.Find("LeftHandCanvas").GetComponent<AudioSource>();
    }

    void Update()
    {

        timerText.text = currentGame.LeftTime.ToString("F2");
        // 매 초 타이머 감소 애니메이션
        if(currentGame.IsOn && currentGame.LeftTime < timerInt)
        {
            timerTextAnim.Enlarge(3f);
            timerInt = (int)currentGame.LeftTime;
        }

        foreach(TextMeshProUGUI score in scoreTexts)
            score.text = currentGame.Score.ToString();
    }

    public void MakeDamagePopup(int damage)
    {
        // 팝업 생성
        GameObject popupObj = Instantiate(scorePopup, popupPos.position, Quaternion.identity, this.transform);
        popupObj.transform.LookAt(cam.transform);
        TextMeshProUGUI popup = popupObj.GetComponentInChildren<TextMeshProUGUI>();
        popup.text = damage.ToString();
        popup.fontSize = Mathf.Lerp(minSize, maxSize, (float)damage / 100);

        // 스코어 크기 애니메이션
        if(damage < 100)
            scoreTextAnim.Enlarge(1);
        else
            scoreTextAnim.Enlarge(3f);

        // 컨트롤러 진동
        StartCoroutine(leftController.VibrateController(0.05f, 0.3f, 0.2f, OVRInput.Controller.LTouch));

        // 효과음 재생
        leftHandAudio.volume = Mathf.Lerp(0.2f, 1f, (float)damage / 100);
        leftHandAudio.Play();

    }
}

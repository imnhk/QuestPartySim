using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    static public UIManager Instance { get { return _instance; } }

    public Camera cam;

    [Header("UI Gameobject")]
    [SerializeField]
    private Phone phone;

    [Header("Help UI")]
    [SerializeField]
    private GameObject helpImage;

    [Header("UI Text")]
    [SerializeField]
    private Canvas leftHandCanvas;

    [SerializeField]
    private TextMeshProUGUI timerText;
    private AnimateText timerTextAnim;
    private int timerInt;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    private AnimateText scoreTextAnim;

    [SerializeField]
    private TextMeshProUGUI alcoholText;

    // Popups
    [Header("Score Popup")]
    [SerializeField]    GameObject scorePopup;
    [SerializeField]    int minSize = 10, maxSize = 20;
    [SerializeField]    Transform popupPos;

    // sfx
    [Header("Effects")]
    private OVRGrabber leftController;
    private AudioSource leftHandAudio;

    private GameManager game;
    

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        leftController = GameObject.Find("DistanceGrabHandLeft").GetComponent<OVRGrabber>();
        leftHandAudio = GameObject.Find("LeftHandCanvas").GetComponent<AudioSource>();

        scoreTextAnim = scoreText.gameObject.GetComponent<AnimateText>();
        timerTextAnim = timerText.gameObject.GetComponent<AnimateText>();


    }

    void Start()
    {
        game = GameManager.Instance;
        timerInt = (int)game.LeftTime;

        leftHandCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        timerText.text = game.LeftTime.ToString("F2");
        alcoholText.text = game.Alcohol.ToString("F3");
        scoreText.text = game.Score.ToString();

        // 매 초 타이머 감소 애니메이션
        if(game.IsOn && game.LeftTime < timerInt)
        {
            timerTextAnim.Enlarge(3f);
            timerInt = (int)game.LeftTime;
        }

    }

    public void HideLeftHandPhone()
    {
        leftHandCanvas.gameObject.SetActive(true);
        phone.gameObject.SetActive(false);
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

    public IEnumerator showHelp(float time)
    {
        helpImage.SetActive(true);
        yield return new WaitForSeconds(time);
        helpImage.SetActive(false);
    }
}

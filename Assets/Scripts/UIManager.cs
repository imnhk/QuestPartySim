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
    [SerializeField]
    private TextMeshProUGUI strengthUI;
    [SerializeField]
    private List<TextMeshProUGUI> scoreTexts;
    private AnimateText scoreTextAnim;

    // Buttons
    [Header("Button")]
    public Button startButton;

    // Popups
    [Header("Score Popup")]
    [SerializeField]
    GameObject scorePopup;
    [SerializeField]
    int minSize = 10, maxSize = 20;
    [SerializeField]
    Transform popupPos;
    [SerializeField]
    Camera cam;

    // sfx
    [Header("Effects")]
    


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
        timerTextAnim = timerText.gameObject.GetComponent<AnimateText>();
        timerInt = (int)currentGame.LeftTime;
        scoreTextAnim = scoreTexts[0].gameObject.GetComponent<AnimateText>();

    }

    void Update()
    {
        timerText.text = currentGame.LeftTime.ToString("F2");
        // 매 초 타이머 감소 애니메이션
        if( currentGame.LeftTime < timerInt)
        {
            timerTextAnim.Enlarge(1.2f);
            timerInt = (int)currentGame.LeftTime;
        }

        strengthUI.text = currentGame.Str.ToString();

        foreach(TextMeshProUGUI score in scoreTexts)
            score.text = currentGame.Score.ToString();
    }

    public void MakeDamagePopup(int damage)
    {
        GameObject popupObj = Instantiate(scorePopup, popupPos.position, Quaternion.identity, this.transform);
        popupObj.transform.LookAt(cam.transform);
        
        TextMeshProUGUI popup = popupObj.GetComponentInChildren<TextMeshProUGUI>();
        popup.text = damage.ToString();
        popup.fontSize = Mathf.Lerp(minSize, maxSize, (float)damage / 100);

        if(damage < 100)
            scoreTextAnim.Enlarge(1.1f);
        else
            scoreTextAnim.Enlarge(1.3f);
            
    }
}

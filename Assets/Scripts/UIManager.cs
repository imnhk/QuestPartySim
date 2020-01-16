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
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    List<TextMeshProUGUI> scoreTexts;

    // Buttons
    public Button startButton;

    // Popups
    [SerializeField]
    GameObject damagePopup;
    [SerializeField]
    Camera cam;

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
    }

    void Update()
    {
        timerText.text = currentGame.timer.LeftTime.ToString("F1");
        foreach(TextMeshProUGUI score in scoreTexts)
            score.text = currentGame.Score.ToString();
    }

    public void MakeDamagePopup(Vector3 pos, int damage)
    {
        if (GameManager.Instance.game.IsOver)
            return;

        GameObject popupObj = Instantiate(damagePopup, pos, Quaternion.identity, this.transform);
        popupObj.transform.LookAt(cam.transform);
        TextMeshProUGUI popup = popupObj.GetComponentInChildren<TextMeshProUGUI>();
        popup.text = damage.ToString();
    }
}

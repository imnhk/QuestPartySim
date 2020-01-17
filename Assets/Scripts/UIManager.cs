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
    TextMeshProUGUI timerUI;
    [SerializeField]
    TextMeshProUGUI strengthUI;
    [SerializeField]
    List<TextMeshProUGUI> scoreUI;

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
        timerUI.text = currentGame.LeftTime.ToString("F1");

        strengthUI.text = currentGame.Str.ToString();

        foreach(TextMeshProUGUI score in scoreUI)
            score.text = currentGame.Score.ToString();
    }

    public void MakeDamagePopup(Vector3 pos, int damage)
    {
        if (GameManager.Instance.game.IsOn == false)
            return;

        GameObject popupObj = Instantiate(damagePopup, pos, Quaternion.identity, this.transform);
        popupObj.transform.LookAt(cam.transform);
        TextMeshProUGUI popup = popupObj.GetComponentInChildren<TextMeshProUGUI>();
        popup.text = damage.ToString();
    }
}

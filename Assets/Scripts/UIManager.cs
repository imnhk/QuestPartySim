using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    static public UIManager Instance { get { return _instance; } }

    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI scoreText;

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
        scoreText.text = currentGame.score.ToString();
    }

    public void MakeDamagePopup(Vector3 pos, int damage)
    {
        GameObject popupObj = Instantiate(damagePopup, pos, Quaternion.identity, this.transform);
        popupObj.transform.LookAt(cam.transform);
        TextMeshProUGUI popup = popupObj.GetComponentInChildren<TextMeshProUGUI>();
        popup.text = damage.ToString();
    }
}

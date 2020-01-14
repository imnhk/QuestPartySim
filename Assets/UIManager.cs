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
    GameObject damagePopup;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        timerText.text = GameManager.Instance.timer.LeftTime.ToString("F1");
    }

    

    public void MakeDamagePopup(Vector3 pos, int damage)
    {
        GameObject popupObj = Instantiate(damagePopup, pos, Quaternion.identity, this.transform);
        TextMeshProUGUI popup = popupObj.GetComponentInChildren<TextMeshProUGUI>();
        popup.text = damage.ToString();
    }
}

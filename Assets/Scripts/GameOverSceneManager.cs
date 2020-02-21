using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverSceneManager : MonoBehaviour
{

    [SerializeField]
    private GameObject camObject;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Image typeImage;
    [SerializeField]
    private List<Sprite> typeImages;

    [SerializeField]
    private AudioClip gameoverSound;
    private AudioSource audioSrc;

    [Header("Ranking")]
    [SerializeField]
    private List<TextMeshProUGUI> rankText;
    [SerializeField]
    private List<TextMeshProUGUI> dateText;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        scoreText.text = GameStats.latestScore.ToString();
        typeImage.sprite = typeImages[GameStats.gameoverType];

        if (gameoverSound)
        {
            audioSrc.PlayOneShot(gameoverSound);
        }

        GameStats.LoadRank();
        GameStats.UpdateRank();
        UpdateRankPanel();
        GameStats.SaveRank();
    }

    private void UpdateRankPanel()
    {
        for(int i=0; i<rankText.Count; i++)
        {
            rankText[i].text = GameStats.rankData.score[i].ToString();
            dateText[i].text = GameStats.rankData.date[i].ToString("yyyy/MM/dd");
            if(GameStats.rankData.score[i] == 0)
            {
                dateText[i].text = "-";
            }
        }
    }

    public void RestartGame()
    {
        StartCoroutine(loadGameWait(1f));
    }

    IEnumerator loadGameWait(float time)
    {
        Debug.Log("restart");
        camObject.GetComponent<OVRScreenFade>().fadeTime = time;
        camObject.GetComponent<OVRScreenFade>().FadeOut();
        yield return new WaitForSeconds(time);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverSceneManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private GameObject camObject;


    private void Start()
    {
        camObject = GameObject.Find("CenterEyeAnchor");

        scoreText.text = GameStats.latestScore.ToString();
    }

    public void RestartGame()
    {
        camObject.GetComponent<OVRScreenFade>().FadeOut(0.5f);
        StartCoroutine(loadGameWait(0.5f));
    }

    IEnumerator loadGameWait(float second)
    {
        yield return new WaitForSeconds(second);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

}

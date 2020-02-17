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
        StartCoroutine(loadGameWait(0.5f));
    }

    IEnumerator loadGameWait(float second)
    {
        camObject.GetComponent<OVRScreenFade>().fadeTime = 0.5f;
        camObject.GetComponent<OVRScreenFade>().FadeOut();
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}

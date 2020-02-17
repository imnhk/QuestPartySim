using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class AlcoholEffects : MonoBehaviour
{
    // 0.0 ~ 0.3
    private float alcohol;

    private Transform cameraRig;
    private OVRScreenFade screenFade;


    private void Awake()
    {
        cameraRig = GameObject.Find("OVRCameraRig").GetComponent<Transform>();
        screenFade = GameObject.Find("CenterEyeAnchor").GetComponent<OVRScreenFade>();
    }

    private void Start()
    {
        
    }


    void Update()
    {
        alcohol = GameManager.Instance.Alcohol;

        if (alcohol > 0.05f)
        {
            RandomCameraTurn();
            RandomBlink();
        }
    }


    private Quaternion nextRotation = Quaternion.Euler(0, 0, 0);
    private float turningTime;
    private float timeUntilTurn = 0;

    void RandomCameraTurn()
    {
        if(timeUntilTurn <= 0)
        {
            turningTime = 1 - (alcohol * 2);
            float rotRange = alcohol * 50;

            nextRotation = Quaternion.Euler(Random.Range(-rotRange, rotRange), Random.Range(-rotRange, rotRange), Random.Range(-rotRange, rotRange));
            timeUntilTurn = turningTime;

            return;
        }
 
        cameraRig.localRotation = Quaternion.Lerp(nextRotation, cameraRig.localRotation, timeUntilTurn / turningTime);
        timeUntilTurn -= Time.deltaTime;
    }

    private float blinkTime;
    private float timeUntilBlink = 0;

    void RandomBlink()
    {
        if(timeUntilBlink <= 0)
        {
            StartCoroutine(Blink(alcohol * 3));
            timeUntilBlink = 2f + Random.Range(0, 3f);
            return;
        }

        timeUntilBlink -= Time.deltaTime;

    }

    IEnumerator Blink(float duration)
    {
        float temp = screenFade.fadeTime;
        screenFade.fadeTime = duration / 2;
        screenFade.FadeOut();
        yield return new WaitForSeconds(duration / 2);
        screenFade.FadeIn();
        yield return new WaitForSeconds(duration / 2);
        screenFade.fadeTime = temp;
    }

}

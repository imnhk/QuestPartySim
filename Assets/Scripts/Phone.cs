using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField]    List<Material>  screens;
    [SerializeField]    GameObject  helpImage;
    [SerializeField]    AudioClip   touchSound;
    [SerializeField]    AudioClip   ringTone;

    private AudioSource audioSrc;
    private Renderer ren;

    private int screenIndex = 0;
    private bool gettingCall = true;


    void Awake()
    {
        ren = GetComponent<Renderer>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(Call());
    }

    private void FixedUpdate()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            PrevScreen();
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            NextScreen();
        }
    }

    public void NextScreen()
    {
        if(screenIndex < screens.Count - 1)
        {
            screenIndex++;
            audioSrc.PlayOneShot(touchSound);
            ren.material = screens[screenIndex];
            Vibrate(0.05f);
        }

        if(screenIndex == 1)
        {
            gettingCall = false;
            audioSrc.Stop();
            helpImage.SetActive(false);
        }
    }
    public void PrevScreen()
    {
        if (screenIndex > 0)
        {
            screenIndex--;
            audioSrc.PlayOneShot(touchSound);
            ren.material = screens[screenIndex];
            Vibrate(0.05f);
        }

        if (screenIndex == 0)
        {
            helpImage.SetActive(true);
        }
    }

    private IEnumerator Call()
    {
        while (gettingCall)
        {
            audioSrc.Play();
            Vibrate(0.5f);
            yield return new WaitForSeconds(1);
        }

    }

    private void Vibrate(float duration)
    {
        StartCoroutine(VibrateController(duration, 0.5f, 0.5f, OVRInput.Controller.LTouch));
    }
    private IEnumerator VibrateController(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);

    }

}

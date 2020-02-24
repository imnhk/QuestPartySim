using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpeech : MonoBehaviour
{
    public SpriteRenderer ren;
    public Sprite speechSprite;
    public Canvas canvas;
    public float visibleDistance = 3f;

    private Transform camPos;
    private float camDistance;
    private void Awake()
    {
        ren.sprite = speechSprite;
        canvas.GetComponentInChildren<Canvas>();
        camPos = GameObject.Find("CenterEyeAnchor").transform;
    }

    private void Update()
    {
        camDistance = Vector3.Distance(camPos.position, this.gameObject.transform.position);
        if(camDistance < visibleDistance)
        {
            ShowCanvas();
        }
        else
        {
            HideCanvas();
        }

        if (canvas.gameObject.activeInHierarchy)
        {
            canvas.transform.LookAt(camPos);
            canvas.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    public void ShowCanvas()
    {
        if (!canvas.gameObject.activeInHierarchy)
            canvas.gameObject.SetActive(true);
    }
    public void HideCanvas()
    {
        if (canvas.gameObject.activeInHierarchy)
            canvas.gameObject.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimateText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float defaultFontSize;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        defaultFontSize = text.fontSize;
    }

    void Update()
    {
        if (text.fontSize > defaultFontSize)
            text.fontSize -= Time.deltaTime * 20;
        
    }

    public void Enlarge(float ratio)
    {
        text.fontSize *= ratio;
    }
}

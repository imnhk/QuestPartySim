using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public List<Material> screens;
    private int currentScreen = 0;

    private Renderer ren;

    void Awake()
    {
        ren = GetComponent<Renderer>();
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
        Debug.Log(screens.Count);
        if(currentScreen < screens.Count - 1)
        {
            currentScreen++;
            ren.material = screens[currentScreen];
        }
    }
    public void PrevScreen()
    {
        if (currentScreen > 0)
        {
            currentScreen--;
            ren.material = screens[currentScreen];
        }
    }

}

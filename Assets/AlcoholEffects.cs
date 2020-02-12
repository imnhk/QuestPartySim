using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class AlcoholEffects : MonoBehaviour
{
    // 0.0 ~ 0.3
    public float alcohol;

    private PostProcessVolume volume;
    private PostProcessProfile profile;
    private Camera cam;

    private Bloom bloom;
    private DepthOfField dof;
    private Grain grain;

    private void Awake()
    {
        cam = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
        volume = GetComponent<PostProcessVolume>();

        bloom = volume.profile.GetSetting<Bloom>();
        dof = volume.profile.GetSetting<DepthOfField>();
        grain = volume.profile.GetSetting<Grain>();
    }

    private void Start()
    {

    }

    void Update()
    {
        alcohol = GameManager.Instance.Alcohol;

        //cam fov

        bloom.intensity.value = alcohol * 80;
        // dof
        grain.intensity.value = alcohol * 3;
        grain.size.value = alcohol * 10;
        
    }
}

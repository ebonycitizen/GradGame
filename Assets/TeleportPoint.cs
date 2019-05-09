using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public Color normalColor;
    public Color hitColor;

    Material material;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        material.SetColor("_TintColor", normalColor);
    }

    // Update is called once per frame
    void Update()
    {
        if(RayFromCamera.ObjectLookAt() == this.gameObject)
            material.SetColor("_TintColor", hitColor);
        else
            material.SetColor("_TintColor", normalColor);
    }
}

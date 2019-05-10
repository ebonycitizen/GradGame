using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public Color normalColor;
    public Color hitColor;

    public Transform parent;
    public Transform target;

    public float distance;

    Material material;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        material.SetColor("_TintColor", normalColor);
        Vector2 vec = new Vector2(target.position.x - parent.position.x, target.position.z - parent.position.z);
        vec.Normalize();
        vec *= distance;
        transform.position = new Vector3(parent.position.x + vec.x, transform.position.y, parent.position.z + vec.y);
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

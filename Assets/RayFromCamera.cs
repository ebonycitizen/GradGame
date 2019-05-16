using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFromCamera : MonoBehaviour
{
    public Transform origin;
    public int targetLayer;

    private static GameObject hitObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hitObj = null;
        int layer = 1 << targetLayer;

        RaycastHit hit;
        bool isHit = Physics.Raycast(origin.position, origin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layer);

        Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.forward) * 50, Color.yellow);


        if (isHit)
        {

            hitObj = hit.transform.gameObject;
            Debug.Log(hitObj.name);
        }
    }

    public static GameObject ObjectLookAt()
    {
        return hitObj;
    }
}

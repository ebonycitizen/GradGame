using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraController : MonoBehaviour
{
    private Teleport teleport;

    public SteamVR_Action_Boolean trigger;

    [SerializeField]
    private float moveRequired;

    private float defaultPos;

    // Start is called before the first frame update
    void Start()
    {
        teleport = Object.FindObjectOfType<Teleport>();
        defaultPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (RayFromCamera.ObjectLookAt() != null && teleport.CanTeleport)
        {
            GameObject obj = RayFromCamera.ObjectLookAt();

            if (defaultPos - transform.position.y >= moveRequired)
            {
                teleport.Begin(obj);
                teleport.CanTeleport = false;
            }
        }

        if(trigger.GetStateDown(SteamVR_Input_Sources.Any))
        {
            defaultPos = transform.position.y;
            Debug.Log("aaa");
        }
    }


}

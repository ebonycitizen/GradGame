using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WireInput : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Input_Sources HandType;

    [SerializeField]
    private SteamVR_ActionSet myAction;
    [SerializeField]
    private SteamVR_Action_Boolean Trigger;

    [SerializeField]
    private GameObject wirePrefab;
    [SerializeField]
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        myAction.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Rope") == null && RayFromCamera.ObjectLookAt() != null && Trigger.GetStateDown(HandType))
        {
            GameObject wire = Instantiate(wirePrefab, camera.transform.position, Quaternion.identity);
            WireAction wireAction = wire.GetComponentInChildren<WireAction>();
            wireAction.Initialize(RayFromCamera.ObjectLookAt(), camera.transform.position);
        }
    }
}

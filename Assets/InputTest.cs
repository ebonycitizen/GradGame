using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputTest : MonoBehaviour
{
    public SteamVR_Input_Sources HandType;

    public SteamVR_ActionSet myAction;
    public SteamVR_Action_Pose Pose;
    public SteamVR_Action_Boolean Teleport;
    public SteamVR_Action_Boolean Trigger;

    public SteamVR_Action_Vibration vibration;

    public GameObject targetObjects;

    private GameObject[] teleportPoints;

    private Teleport teleport;

    // Start is called before the first frame update
    void Start()
    {
        teleport = Object.FindObjectOfType<Teleport>();

        myAction.Activate();

         targetObjects.SetActive(false);

        teleportPoints = GameObject.FindGameObjectsWithTag("TeleportPoint");
        foreach (GameObject obj in teleportPoints)
            obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Pose.GetVelocity(HandType).y);
        if (RayFromCamera.ObjectLookAt() != null && teleport.CanTeleport)
        {
            //Debug.Log(Pose.GetLocalPosition(HandType).y + "  " + Pose.GetVelocity(HandType).y);
            GameObject obj = RayFromCamera.ObjectLookAt();
            //if (Pose.GetLocalPosition(HandType).y <= teleport.heightRequired &&
            //    (Pose.GetLastLocalPosition(HandType).y - Pose.GetLocalPosition(HandType).y) > 0 &&

            //    (Pose.GetLastVelocity(HandType).y - Pose.GetVelocity(HandType).y) > 0 && 
            //    Pose.GetVelocity(HandType).y <= teleport.speedRequired)

            //if((Pose.GetVelocity(HandType).y > 1 && Pose.GetLocalPosition(HandType).y > 0.1f) || Trigger.GetStateDown(SteamVR_Input_Sources.Any))

            if (Pose.GetLocalPosition(HandType).y <= teleport.heightRequired &&
                (Pose.GetLastLocalPosition(HandType).y - Pose.GetLocalPosition(HandType).y) > 0 &&

                (Pose.GetLastVelocity(HandType).y - Pose.GetVelocity(HandType).y) > 0 &&
                Pose.GetVelocity(HandType).y <= teleport.speedRequired)
            {
                teleport.Begin(obj);
                teleport.CanTeleport = false;
            }
        }

        ShowTargetObject();
    }

    private void ShowTargetObject()
    {
        if (Teleport.GetState(SteamVR_Input_Sources.LeftHand) || Teleport.GetState(SteamVR_Input_Sources.RightHand))
        {
            targetObjects.SetActive(true);
            foreach(GameObject obj in teleportPoints)
                obj.SetActive(true);
        }
        else
        {
            targetObjects.SetActive(false);
            foreach (GameObject obj in teleportPoints)
                obj.SetActive(false);
        }         
    }
}

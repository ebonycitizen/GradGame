using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FixedPosition : MonoBehaviour
{
    public SteamVR_Action_Pose Pose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Pose.GetLocalPosition(SteamVR_Input_Sources.LeftHand);
    }
}

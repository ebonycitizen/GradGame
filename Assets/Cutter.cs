using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Cutter : MonoBehaviour
{
    public SteamVR_Action_Pose pose;

    public SteamVR_Input_Sources HandType;

    public SteamVR_Action_Vibration Vibration;

    private Vector3 prePos = Vector3.zero;
    private Vector3 prePos2 = Vector3.zero;

    public float time = 0.2f;
    public int herz = 60;
    public float power = 1;



    // Update is called once per frame
    void FixedUpdate()
    {
        prePos = prePos2;
        prePos2 = transform.position;
    }

    // このコンポーネントを付けたオブジェクトのCollider.IsTriggerをONにすること！
    void OnTriggerEnter(Collider other)
    {
        var meshCut = other.gameObject.GetComponent<MeshCut>();
        if (meshCut == null) { return; }

        if (pose.GetVelocity(HandType).magnitude <= 1)
        {
            other.GetComponent<Rigidbody>().velocity = pose.GetVelocity(HandType) * 10;
            return;
        }
        var cutPlane = new Plane(Vector3.Cross(transform.forward.normalized, prePos - transform.position).normalized, transform.position);
        meshCut.Cut(cutPlane, pose.GetVelocity(SteamVR_Input_Sources.RightHand));

        Vibration.Execute(0, time, herz, power, HandType);
    }

}
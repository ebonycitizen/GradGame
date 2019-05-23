using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Skateboard : MonoBehaviour
{
    [SerializeField]
    private SteamVR_ActionSet myAction;

    [SerializeField]
    private SteamVR_Input_Sources RightFoot;
    [SerializeField]
    private SteamVR_Input_Sources LeftFoot;
    [SerializeField]
    public SteamVR_Action_Pose PoseRightFoot;
    [SerializeField]
    public SteamVR_Action_Pose PoseLeftFoot;

    [SerializeField]
    private Transform cameraVr;
    [SerializeField]
    private Transform cameraRig;

    [SerializeField]
    private float rotateDisRequired;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotateAngle;

    private Vector2 cameraPos;
    private Vector2 footDirection;
    private Vector2 rightFoot;
    private Vector2 leftFoot;
    private Vector2 moveDirection;

    private enum CameraSide
    {
        Right = 1, Left = -1
    };

    private void SetMoveDirection()
    {
        Vector3 right = PoseRightFoot.GetLocalPosition(RightFoot);
        Vector3 left = PoseLeftFoot.GetLocalPosition(LeftFoot);

        rightFoot = new Vector2(right.x, right.z);
        leftFoot = new Vector2(left.x,left.z);

        footDirection = (rightFoot - leftFoot).normalized;
        moveDirection = footDirection;
    }

    private float CrossProuct(Vector2 ab, Vector2 ap)
    {
        return ab.x * ap.y - ab.y * ap.x;
    }

    private CameraSide GetCameraSide()
    { 
        Vector2 cameraFootVec = (cameraPos - leftFoot).normalized;

        float crossProduct = CrossProuct(footDirection, cameraFootVec);

        if (crossProduct < 0)
            return CameraSide.Right;
        else
            return CameraSide.Left;
    }

    private float GetDistanceFromCenter()
    {
        cameraPos = new Vector2(cameraVr.localPosition.x, cameraVr.localPosition.z);
        Vector2 footVec = rightFoot - leftFoot;
        Vector2 cameraFootVec = cameraPos - leftFoot;

        float surfaceArea = Mathf.Abs(CrossProuct(footVec, cameraFootVec));
        float footDistance = Vector2.Distance(rightFoot, leftFoot);

        return surfaceArea / footDistance;
    }

    private void Move()
    {
        Vector2 rotateVec = Vector2.zero;
        Vector2 vec = Vector2.zero;

        if (GetDistanceFromCenter() > rotateDisRequired)
        {
            if (GetCameraSide() == CameraSide.Right)
                vec = new Vector2(moveDirection.y, -moveDirection.x);
            else
                vec = new Vector2(-moveDirection.y, moveDirection.x);
        }
        else
        {
            vec = Vector2.zero;
        }

        //rotateVec.x = moveDirection.x * Mathf.Cos(angle) - moveDirection.y * Mathf.Sin(angle);
        //rotateVec.y = moveDirection.y * Mathf.Cos(angle) + moveDirection.x * Mathf.Sin(angle);
        //moveDirection = rotateVec;

        Vector3 moveVec = new Vector3(moveDirection.x + vec.x * rotateAngle, 0f, moveDirection.y + vec.y * rotateAngle);
        cameraRig.position += moveVec * speed * Time.deltaTime;
    }

    private void OnEnable()
    {
        SetMoveDirection();
    }

    private void Awake()
    {
        myAction.Activate();
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}

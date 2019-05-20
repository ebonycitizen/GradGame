using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireAction : MonoBehaviour
{
    private Vector3 velocity;
    private Transform target;
    private Vector3 cameraPosition;

    [SerializeField]
    private float period;

    [SerializeField]
    private float distanceInterval;

    [SerializeField]
    private GameObject wirePrefab;

    private Vector3 wireDirection;

    private CableComponent cableComponent;


    // Start is called before the first frame update
    void Start()
    {
        wireDirection = target.transform.position - cameraPosition;
        cableComponent = GetComponent<CableComponent>();
    }

    public void Initialize(GameObject target, Vector3 position)
    {
        this.target = target.transform;
        cameraPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period < 0f)
            return;

        Shot();
    }

    private void Shot()
    {
        Vector3 acceleration = Vector3.zero;
        Vector3 diff = target.position - cameraPosition;

        acceleration += (diff - velocity * period) * 2f / (period * period);

        period -= Time.deltaTime;

        velocity += acceleration * Time.deltaTime;
        cameraPosition += velocity * Time.deltaTime;

        transform.position = cameraPosition;
    }

    //private int ComplementWire()
    //{
    //    int wireNumber = 0;

    //    float distance = wireDirection.magnitude;

    //    wireNumber =  Mathf.CeilToInt(distance / distanceInterval);

    //    return wireNumber;
    //}

    //private Vector3 AbsPosition()
    //{
    //    Vector3 direction = (cameraPosition - target.position).normalized;

    //    Vector3 absPosition = direction * distanceInterval;

    //    return absPosition;
    //}

    //private void CreateWire()
    //{
    //    int wireNumber = ComplementWire();
    //    Vector3 absPosition = AbsPosition();
    //    Vector3 refPos = transform.position; //最初の相対位置
    //    HingeJoint refHingeJoint = GetComponent<HingeJoint>();

    //    GameObject[] rope = new GameObject[wireNumber + 1]; // +1はWireParentの分

    //    rope[0] = this.gameObject;

    //    for (int i = 0; i < wireNumber; i++)
    //    {
    //        Vector3 spawnPos = refPos + absPosition;
    //        GameObject obj = Instantiate(wirePrefab, spawnPos, Quaternion.identity, transform);

    //        rope[i + 1] = obj;

    //        refHingeJoint.connectedBody = obj.GetComponent<Rigidbody>();

    //        refPos = obj.transform.position;
    //        refHingeJoint = obj.GetComponent<HingeJoint>();
    //    }
    //    ropeLineRenderer.Init(rope.Length, rope);
    //}

}

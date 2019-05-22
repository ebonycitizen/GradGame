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
    private float speed;

    [SerializeField]
    private float distanceOffset;

    //[SerializeField]
    //private float distanceInterval;

    //[SerializeField]
    //private GameObject wirePrefab;

    private Vector3 wireDirection;

    private CableComponent cableComponent;

    private Vector3 position;


    // Start is called before the first frame update
    void Start()
    {
        wireDirection = target.transform.position - cameraPosition;
        cableComponent = GetComponent<CableComponent>();
        position = transform.position;
    }

    public void Initialize(GameObject target, Vector3 position)
    {
        this.target = target.transform;
        cameraPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        SetCableLength();

        if (period < 0f)
            return;

        Shot();
    }

    private void Shot()
    {
        Vector3 acceleration = Vector3.zero;
        Vector3 diff = target.position - position;

        acceleration += (diff - velocity * period) * 2f / (period * period);

        period -= Time.deltaTime;

        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        transform.position = position;
    }

    private void SetCableLength()
    {
        float length = Vector3.Distance(cameraPosition, transform.position);

        cableComponent.CableLength = length - 0.2f;
    }

    private void GetClose()
    {
        StartCoroutine("MoveClose");
    }

    IEnumerator MoveClose()
    {
        float distance = Vector3.Distance(transform.position, cameraPosition);

        while (Vector3.Distance(transform.position, cameraPosition) > distanceOffset)
        {
            //float currentPos = (Time.time * speed) / distance;

            transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * speed);
            //transform.position = Vector3.MoveTowards(transform.position, camera.position, speed * Time.deltaTime);

            yield return null;
        }
        target.transform.parent = null;
        Destroy(transform.parent.gameObject, 0.2f);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            target.transform.parent = transform;
            GetClose();
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform camera;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float distanceOffset;

    private Vector3 direction;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        direction = (camera.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetClose()
    {
        StartCoroutine("MoveClose");
    }

    IEnumerator MoveClose()
    {

        float distance = Vector3.Distance(transform.position, camera.position);

        while (Vector3.Distance(transform.position, camera.position) > distanceOffset)
        {
            //float currentPos = (Time.time * speed) / distance;

            transform.position = Vector3.Lerp(transform.position, camera.position, Time.deltaTime *speed);
            //transform.position = Vector3.MoveTowards(transform.position, camera.position, speed * Time.deltaTime);

            yield return null;
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Rope"))
            Destroy(obj, 0.2f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rope")
        {
            GetClose();
        }
    }
}

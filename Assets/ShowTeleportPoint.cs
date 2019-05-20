using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTeleportPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private GameObject[] teleportPoints;

    // Start is called before the first frame update
    void Start()
    {
        teleportPoints = GameObject.FindGameObjectsWithTag("TeleportPoint");
        foreach (GameObject obj in teleportPoints)
            obj.SetActive(false);

        Debug.Log(teleportPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            foreach (GameObject obj in teleportPoints)
                obj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            foreach (GameObject obj in teleportPoints)
                obj.SetActive(false);
        }
    }
}

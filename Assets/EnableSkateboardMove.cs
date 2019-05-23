using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class EnableSkateboardMove : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Boolean trigger;

    [SerializeField]
    private float moveHeightRequired;
    [SerializeField]
    private GameObject skateboard;

    private float defaultPos;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.localPosition.y;
        Debug.Log(defaultPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.GetState(SteamVR_Input_Sources.Any) || CanMove())
            skateboard.SetActive(true);
        else
            skateboard.SetActive(false);
    }

    private bool CanMove()
    {
        if (defaultPos - transform.localPosition.y >= moveHeightRequired)
        {
            return true;
        }
        return false;
    }
}

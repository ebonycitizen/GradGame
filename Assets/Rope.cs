using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer line;
    private GameObject[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        vertices = null;
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vertices == null)
            return;

        if (GameObject.FindGameObjectWithTag("Rope") == null)
        {
            line.positionCount = 0;
            vertices = null;

            return;
        }

        int idx = 0;
        foreach (GameObject v in vertices)
        {
            line.SetPosition(idx, v.transform.position);
            idx++;
        }
    }

    public void Init(int size, GameObject[] rope)
    {
        vertices = new GameObject[size];
        vertices = rope;

        line.positionCount = vertices.Length;
    }
}

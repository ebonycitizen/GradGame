using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float heightRequired;//0.1f
    public float speedRequired;//-1f
    public float moveSpeed;

    private bool canTeleport;
    public bool CanTeleport
    {
        get { return canTeleport; }
        set { canTeleport = value; }
    }

    public GameObject target;
    public GameObject teleportPointPrefab;



    // Start is called before the first frame update
    void Start()
    {
        canTeleport = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin(GameObject obj)
    {
        StartCoroutine("Move", obj);
    }

    IEnumerator Move(GameObject obj)
    {

        Vector3 to = obj.transform.position;
        Transform parent = obj.transform.parent;

        //Destroy(obj);

        yield return new WaitForSeconds(0.1f);

        while (Vector3.Distance(target.transform.position, to) > 0.1f)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, to, moveSpeed * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(0.7f);
        canTeleport = true;
        //Spawn(parent);
    }

    private void Spawn(Transform parent)
    {
        float offset = 2f;
        float x = Random.Range(target.transform.position.x - offset, target.transform.position.x + offset);
        float y = target.transform.position.y;
        float z = Random.Range(target.transform.position.z - offset, target.transform.position.z + offset);

        Vector3 spawnPos = new Vector3(x, y, z);

        Instantiate(teleportPointPrefab, spawnPos, Quaternion.identity, parent);
    }
}

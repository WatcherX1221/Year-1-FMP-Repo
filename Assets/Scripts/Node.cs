using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float difficulty;
    public List<GameObject> nodes = new List<GameObject>();
    public List<float> nodeDistances = new List<float>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(nodes.Count != nodeDistances.Count)
        {
            GetNodeDistances();
        }
    }

    public void GetNodeDistances()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodeDistances.Add(Vector2.Distance(this.gameObject.transform.position, nodes[i].transform.position));
        }
    }
    public void GetDistanceToTarget()
    {

    }
}

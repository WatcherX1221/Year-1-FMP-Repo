using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int nodeID;
    public List<GameObject> neighboringNodes;
    public bool findNeighboringNodes = false;
    NodeMapBuilder nodeMapBuilder;
    // Start is called before the first frame update
    void Start()
    {
        findNeighboringNodes = true;
        nodeMapBuilder = GetComponent<NodeMapBuilder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (findNeighboringNodes == true)
        {
            neighboringNodes.Add(nodeMapBuilder.existingNodes[nodeID - 10]);
            neighboringNodes.Add(nodeMapBuilder.existingNodes[nodeID - 1]);
            neighboringNodes.Add(nodeMapBuilder.existingNodes[nodeID + 1]);
            neighboringNodes.Add(nodeMapBuilder.existingNodes[nodeID + 10]);
        }
    }
}

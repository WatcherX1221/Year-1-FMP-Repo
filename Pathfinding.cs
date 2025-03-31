using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField]
    GameObject currentNode;
    [SerializeField]
    int currentNodeID;
    NodeMapBuilder nodeMapBuilder;
    GameObject nodeMapBuild;
    GameObject target;

    // Used for gScore
    int nodesUsed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            GetCurrentNode();
        }
    }

    public void GetCurrentNode()
    {

        nodeMapBuild = GameObject.FindGameObjectWithTag("NodeMapBuilder");
        nodeMapBuilder = nodeMapBuild.GetComponent<NodeMapBuilder>();

        for (int i = 0; i < nodeMapBuilder.existingNodes.Count; i++)
        {
            Vector2 currentPos = transform.position;
            Vector2 nodePos = nodeMapBuilder.existingNodes[i].transform.position;

            if(currentPos == nodePos)
            {
                currentNode = nodeMapBuilder.existingNodes[i];
            }
        }
    }

    public void GetBestNode()
    {
        for (int i = 0; i < currentNode.GetComponent<Node>().neighboringNodes.Count; i++)
        {
            if(currentNode.GetComponent<Node>().neighboringNodes.Count == 2)
            currentNode.GetComponent<Node>().neighboringNodes[i].GetComponent<Node>().fScore;
        }
    }
}
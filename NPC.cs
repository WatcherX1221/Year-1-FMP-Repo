using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public List<GameObject> allNodes = new List<GameObject>();
    public List<float> allNodeDistances = new List<float>();
    public GameObject closestNode;
    public GameObject target;
    public GameObject targetNode;
    public GameObject nearestNodeToTarget;
    public bool moving = false;
    public bool hasMoved = false;
    // Start is called before the first frame update
    void Start()
    {
        moving = true;
        hasMoved = false;
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        if(allNodes.Count == 0)
        {
            GetAllNodes();
        }
        
        if(allNodeDistances.Count == 0)
        {
            GetAllNodeDistance();
        }

        if(allNodes.Count == allNodeDistances.Count && closestNode == null)
        {
            Debug.Log("Call getCloseNode");
            GetCloseNode();
        }

        if(closestNode != null)
        {
            moving = true;
            if(!hasMoved)
            {
                targetNode = closestNode;
                hasMoved = true;
            }
        }

        if(moving)
        {
            Vector2 movement = Vector2.MoveTowards(transform.position, targetNode.transform.position, Time.deltaTime);
            transform.position = movement;
        }

        if(transform.position == targetNode.transform.position)
        {
            GetBestNode();
        }
    }

    public void GetAllNodes()
    {
        allNodes.AddRange(GameObject.FindGameObjectsWithTag("Node"));
    }    

    public void GetAllNodeDistance()
    {
        for(int i = 0; i < allNodes.Count; i++)
        {
            allNodeDistances.Add(Vector2.Distance(this.gameObject.transform.position, allNodes[i].transform.position));
        }
    }

    public void GetCloseNode()
    {
        Debug.Log("Called getCloseNode");

        List<float> distances = new List<float>();
        List<GameObject> relativeNodes = new List<GameObject>();
        GameObject closeNode = null;
        GameObject closeNodeToTarget = null;
        float closestNodeDist = float.PositiveInfinity;
        float closestNodeToTargetDist = float.PositiveInfinity;

        // For getting closest node to the NPC spawn
        for (int i = 0; i < allNodes.Count; i++)
        {
            distances.Add(allNodeDistances[i]);

            relativeNodes.Add(allNodes[i]);
        }

        
        for (int i = 0; i < allNodeDistances.Count; i++)
        {
            if (distances[i] < closestNodeDist)
            {
                closestNodeDist = distances[i];
                closeNode = relativeNodes[i];
            }
            if (i == allNodeDistances.Count - 1)
            {
                closestNode = closeNode;
            }
        }

        // For getting closest node to the target

        for (int i = 0; i < allNodes.Count; i++)
        {
            distances.Add(allNodeDistances[i]);

            relativeNodes.Add(allNodes[i]);
        }

        for (int i = 0; i < allNodes.Count; i++)
        {
            if (distances[i] < closestNodeToTargetDist)
            {
                closestNodeToTargetDist = distances[i];
                closeNodeToTarget = relativeNodes[i];
            }
            if (i == allNodeDistances.Count - 1)
            {
                nearestNodeToTarget = closeNode;
            }
        }


    }

    public void GetBestNode()
    {
        List<float> finalScores = new List<float>();
        List<GameObject> node = new List<GameObject>();
        float bestScore = float.PositiveInfinity;
        GameObject bestNode = null;

        
        for (int i = 0; i < targetNode.GetComponent<Node>().nodes.Count; i++)
        {
            finalScores.Add((targetNode.GetComponent<Node>().nodeDistances[i] + Vector2.Distance(targetNode.GetComponent<Node>().nodes[i].transform.position, target.transform.position)) * targetNode.GetComponent<Node>().nodes[i].GetComponent<Node>().difficulty);
            Debug.Log(finalScores[i]);
            node.Add(targetNode.GetComponent<Node>().nodes[i]);
            Debug.Log(node[i]);
        }

        for (int i = 0; i < targetNode.GetComponent<Node>().nodes.Count; i++)
        {
            if (finalScores[i] < bestScore)
            {
                bestScore = finalScores[i];
                bestNode = node[i];
                Debug.Log(bestNode);
            }
            if (i == allNodeDistances.Count)
            {
                Debug.Log("Best Node is: " + bestNode);
                targetNode = bestNode;
                Debug.Log("Best node is: " + bestNode);
            }
        }


        Debug.Log("Ran GetBestNode");

    }


}

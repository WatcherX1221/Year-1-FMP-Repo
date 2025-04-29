using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public List<GameObject> allNodes = new List<GameObject>();
    public List<float> allNodeDistances = new List<float>();
    public GameObject closestNode;
    public GameObject target;
    public Vector3 targetPos;
    public GameObject targetNode;
    public GameObject nearestNodeToTarget;
    public bool movingToNode = false;
    public bool hasMoved = false;
    public List<GameObject> visitedNodes = new List<GameObject>();
    public bool targetIsObstructed;
    public float distToTN;
    public GameObject player;
    public bool canSeePlayer;
    // Start is called before the first frame update
    void Start()
    {
        movingToNode = true;
        hasMoved = false;
        target = GameObject.FindGameObjectWithTag("Target");
        targetIsObstructed = true;
        player = GameObject.FindGameObjectWithTag("Player");
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
            movingToNode = true;
            if(!hasMoved)
            {
                targetNode = closestNode;
                hasMoved = true;
            }
        }

        if(movingToNode)
        {
            Vector2 movement = Vector2.MoveTowards(transform.position, targetNode.transform.position, Time.deltaTime * 2);
            transform.position = movement;
        }

        distToTN = Vector2.Distance(transform.position, targetNode.transform.position);
        if (distToTN <= 1f && targetNode == allNodes.Contains(targetNode))
        {
            GetBestNode();
        }
        else
        {

        }

        targetIsObstructed = Physics2D.Linecast(transform.position, target.transform.position, LayerMask.GetMask("Solid"));

        if(!targetIsObstructed)
        {
            targetNode = target;
        }
        
        if (targetIsObstructed)
        {
            
        }

        if (transform.position == target.transform.position)
        {
            movingToNode = false;
        }

        if(targetPos != target.transform.position)
        {
            Vector2 movement = Vector2.zero;
            targetPos = target.transform.position;
            targetIsObstructed = true;
            visitedNodes.Clear();
            GetAllNodeDistance();
            GetCloseNode();
            targetNode = closestNode;
        }

        canSeePlayer = !Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Solid"));
    }

    public void GetAllNodes()
    {
        allNodes.Clear();
        allNodes.AddRange(GameObject.FindGameObjectsWithTag("Node"));
    }    

    public void GetAllNodeDistance()
    {
        allNodeDistances.Clear();
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

        
        for (int i = 0; i < distances.Count; i++)
        {
            if (distances[i] < closestNodeDist && !Physics2D.Linecast(transform.position, allNodes[i].transform.position, LayerMask.GetMask("Solid")))
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

        for (int i = 0; i < distances.Count; i++)
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

        for (int i = 0; i < finalScores. Count; i++)
        {
            //Debug.Log("Final score of " + i + " is " + finalScores[i]);
            if (finalScores[i] < bestScore && !visitedNodes.Contains(targetNode.GetComponent<Node>().nodes[i]))
            {
                bestScore = finalScores[i];
                bestNode = node[i];
                Debug.Log(bestNode);
            }
            if (i == targetNode.GetComponent<Node>().nodes.Count - 1)
            {
                Debug.Log("Best Node is: " + bestNode);
                visitedNodes.Add(targetNode);
                targetNode = bestNode;
                Debug.Log("Best node is: " + bestNode);
            }
        }


        Debug.Log("Ran GetBestNode");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Solid")
        {
            Debug.Log("Collided with Solid");
        }
    }


}

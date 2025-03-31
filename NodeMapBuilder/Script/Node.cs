using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int nodeID;
    public List<GameObject> neighboringNodes;
    NodeMapBuilder nodeMapBuilder;
    GameObject nodeMapBuild;
    public bool isValid;
    public bool hasCheckedValid;
    public int difficulty;
    public int gScore;
    public int hScore;
    public int fScore;
    // Start is called before the first frame update
    void Start()
    {
        nodeMapBuild = GameObject.FindGameObjectWithTag("NodeMapBuilder");
        nodeMapBuilder = nodeMapBuild.GetComponent<NodeMapBuilder>();
        isValid = true;
        hasCheckedValid = false;

    }

    

    private void OnCollisionStay2D(Collision2D collision)
    {
        {
            if (hasCheckedValid == false)
            {
                if (collision.collider.tag == "Solid")
                {
                    isValid = false;
                }
                hasCheckedValid = true;
            }


        }
    }

    public void GetNeighbours()
    {

        nodeMapBuild = GameObject.FindGameObjectWithTag("NodeMapBuilder");
        nodeMapBuilder = nodeMapBuild.GetComponent<NodeMapBuilder>();

        for (int i = 0; i < nodeMapBuilder.existingNodes.Count; i++)
        {
            Vector2 currentNodePos = nodeMapBuilder.existingNodes[i].transform.position;
            Vector2 adiacentTop = new Vector2(transform.position.x, transform.position.y +1);
            Vector2 adiacentBottom = new Vector2(transform.position.x, transform.position.y - 1);
            Vector2 adiacentLeft = new Vector2(transform.position.x - 1, transform.position.y);
            Vector2 adiacentRight = new Vector2(transform.position.x + 1, transform.position.y);

            if (nodeID != i && currentNodePos == adiacentTop || currentNodePos == adiacentBottom || currentNodePos == adiacentLeft || currentNodePos == adiacentRight)
            {
                neighboringNodes.Add(nodeMapBuilder.existingNodes[i]);
            }
           

        }
    }

    public void GetFScore()
    {
        fScore






        fScore = gScore + hScore;
    }

    
}

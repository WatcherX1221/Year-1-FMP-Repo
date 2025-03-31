using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMapBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject node;
    public int nodeSizeX;
    public int nodeSizeY;
    public int nodeBuildPosX;
    public int nodeBuildPosY;
    public bool buildNodes = false;
    public int newNodeID;
    bool hasFinishedBuilding;
    public List<GameObject> existingNodes = new List<GameObject>();
    Node nodeScript;
    // Start is called before the first frame update
    void Start()
    {
        buildNodes = false;
        hasFinishedBuilding = false;
        nodeBuildPosX = 0;
        nodeBuildPosY = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            buildNodes = true;
        }
        if(buildNodes == true)
        {
            GameObject newNode = Instantiate(node, new Vector2(nodeBuildPosX, nodeBuildPosY), transform.rotation);
            newNode.GetComponent<Node>().nodeID = newNodeID;
            newNodeID += 1;
            nodeBuildPosX += 1;
            existingNodes.Add(newNode);


            if (nodeBuildPosX >= nodeSizeX && nodeBuildPosY <= -nodeSizeY + 1)
            {
                buildNodes = false;

                if (newNodeID == (nodeSizeX * nodeSizeY))
                {
                    hasFinishedBuilding = true;
                }


                if (hasFinishedBuilding)
                {
                    for (int i = 0; i < existingNodes.Count; i++)
                    {
                        existingNodes[i].GetComponent<Node>().GetNeighbours();
                    }

                }


            }
            if (nodeBuildPosX == nodeSizeX)
            {
                nodeBuildPosY -= 1;
                nodeBuildPosX = 0;
            }

           

        }

        
        




    }

   
}

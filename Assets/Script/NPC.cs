using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public List<GameObject> allNodes = new List<GameObject>();
    public List<float> allNodeDistances = new List<float>();
    public List<GameObject> otherNPCS = new List<GameObject>();
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
    public bool canReact;
    public float reactTimer;
    public float textTimer;
    public TextMeshPro TMP;
    [SerializeField]
    GameObject dialogueSystem;
    public bool followingPlayer;
    public float interestToPlayer;
    public bool tungstenReactions;
    [SerializeField]
    GameObject tungstenHole;
    public float tungstenCD;
    [SerializeField]
    bool plushChase;
    public bool hasPlush;
    public float stealImmunity;
    Animator animator;

    // Start is called before the first frame upsdate
    void Start()
    {
        movingToNode = true;
        hasMoved = false;
        targetIsObstructed = true;
        player = GameObject.FindGameObjectWithTag("Player");
        canReact = true;
        reactTimer = 2f;
        followingPlayer = false;
        tungstenReactions = false;
        tungstenCD = 2f;
        plushChase = false;
        animator = GetComponent<Animator>();
        GetOtherNPCS();
    }

    // Update is called once per frame
    void Update()
    {
        stealImmunity -= Time.deltaTime;

        if (allNodes.Count == 0)
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
            Vector2 movement = Vector2.MoveTowards(transform.position, targetNode.transform.position, Time.deltaTime * 3);
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

        if(targetPos != target.transform.position && !followingPlayer)
        {
            Vector2 movement = Vector2.zero;
            targetPos = target.transform.position;
            visitedNodes.Clear();
            GetAllNodeDistance();
            GetCloseNode();
            targetNode = closestNode;
        }
        if (targetPos != target.transform.position && !followingPlayer)
        {
            Vector2 movement = Vector2.zero;
            targetPos = target.transform.position;
        }

        canSeePlayer = !Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Solid"));

        if (reactTimer > 0)
        {
            reactTimer -= Time.deltaTime;
        }
        else if (reactTimer <= 0)
        {
            canReact = true;
        }
        if(textTimer > 0)
        {
            textTimer -= Time.deltaTime;
        }
        if(textTimer <= 0)
        {
            TMP.text = "";
        }

        if (canSeePlayer)
        {
            if (player.GetComponent<PlayerStateManager>().playerEquippedState == 0)
            {
                followingPlayer = false;
                tungstenReactions = false;
            }
        }
        if (canReact)
        {
            if (player.GetComponent<PlayerStateManager>().playerEquippedState == 4)
            {
                textTimer = 2f;
                canReact = false;
                reactTimer = 2f;
                TMP.text = dialogueSystem.GetComponent<DialogueSystem>().dialogueList[3];
                followingPlayer = false;
                tungstenReactions = false;
                target.transform.position = player.transform.position;
            }
            if (hasPlush)
            {
                reactTimer = 3f;
                canReact = false;
                int direction = Random.Range(0, 4);
                if (direction == 0)
                {
                    target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + -6f, transform.position.z);
                }
                if (direction == 1)
                {
                    target.transform.position = new Vector3(target.transform.position.x + -6f, target.transform.position.y, transform.position.z);
                }
                if (direction == 2)
                {
                    target.transform.position = new Vector3(target.transform.position.x + 6f, target.transform.position.y, transform.position.z);
                }
                if (direction == 3)
                {
                    target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 6f, transform.position.z);
                }
            }
            if (canSeePlayer)
            {
                if (player.GetComponent<PlayerStateManager>().playerEquippedState == 1)
                {
                    textTimer = 2f;
                    canReact = false;
                    reactTimer = 2f;
                    TMP.text = dialogueSystem.GetComponent<DialogueSystem>().dialogueList[0];
                    followingPlayer = true;
                    tungstenReactions = false;
                }
                if (player.GetComponent<PlayerStateManager>().playerEquippedState == 2)
                {
                    textTimer = 3f;
                    canReact = false;
                    reactTimer = 2f;
                    TMP.text = dialogueSystem.GetComponent<DialogueSystem>().dialogueList[1];
                    int direction = Random.Range(0, 4);
                    if (direction == 0)
                    {
                        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + -10, transform.position.z);
                    }
                    if (direction == 1)
                    {
                        target.transform.position = new Vector3(target.transform.position.x + -10, target.transform.position.y , transform.position.z);
                    }
                    if (direction == 2)
                    {
                        target.transform.position = new Vector3(target.transform.position.x + 10, target.transform.position.y, transform.position.z);
                    }
                    if (direction == 3)
                    {
                        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 10, transform.position.z);
                    }
                    followingPlayer = false;
                    tungstenReactions = false;
                }
                if (player.GetComponent<PlayerStateManager>().playerEquippedState == 3)
                {
                    textTimer = 2f;
                    canReact = false;
                    reactTimer = 2f;
                    TMP.text = dialogueSystem.GetComponent<DialogueSystem>().dialogueList[2];
                    followingPlayer = false;
                    tungstenReactions = true;
                }

                if (player.GetComponent<PlayerStateManager>().playerEquippedState == 5)
                {
                    textTimer = 2f;
                    canReact = false;
                    reactTimer = 2f;
                    TMP.text = dialogueSystem.GetComponent<DialogueSystem>().dialogueList[4];
                    followingPlayer = false;
                    tungstenReactions = false;
                    plushChase = true;
                }
            }
        }

        if (followingPlayer)
        {
            if(canSeePlayer)
            {
                target.transform.position = player.transform.position;
                interestToPlayer = 5f;
            }
            else if(!canSeePlayer)
            {
                interestToPlayer -= Time.deltaTime;
            }

            if(interestToPlayer <= 0f)
            {
                followingPlayer = false;
            }
        }

        if (tungstenReactions)
        {
            tungstenCD -= Time.deltaTime;
            if (Vector2.Distance(transform.position, player.transform.position) <= 3)
            {
                TMP.text = dialogueSystem.GetComponent<DialogueSystem>().dialogueList[5];
                textTimer = 2f;
            }
            if (Vector2.Distance(transform.position, player.transform.position) <= 1.5f)
            {
                if(tungstenCD <= 0)
                {
                    GameObject holeClone = Instantiate(tungstenHole, transform.position, transform.rotation);
                    Destroy(holeClone, 1.5f);
                    tungstenCD = 2f;
                }
                
            }

        }

        for (int i = 0; i < otherNPCS.Count; i++)
        {
            if(Physics2D.Linecast(transform.position, otherNPCS[i].transform.position, LayerMask.GetMask("Solid")))
            {
                if (otherNPCS[i].GetComponent<NPC>().hasPlush)
                {
                    plushChase = true;
                }
            }
        }

        if (plushChase)
        {
            PlushChase();
        }
        if(hasPlush)
        {
            animator.SetBool("hasPlush", true);
        }
        else
        {
            animator.SetBool("hasPlush", false);
        }

        if (Physics2D.Linecast(transform.position, target.transform.position, LayerMask.GetMask("Boundary")))
        {
            target.transform.position = transform.position;
        }
        for (int i = 0; i < otherNPCS.Count; i++)
        {
            if (Physics2D.Linecast(transform.position, otherNPCS[i].transform.position, LayerMask.GetMask("Solid")) && Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Solid")))
            {
                plushChase = false;
            }
            else if(Physics2D.Linecast(transform.position, otherNPCS[i].transform.position, LayerMask.GetMask("Solid")) && Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Solid")))
            {
                if (otherNPCS[i].GetComponent<NPC>().hasPlush || player.GetComponent<PlayerStateManager>().playerEquippedState == 5)
                {
                    plushChase = true;
                }
            }

        }
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

    public void GetOtherNPCS()
    {
        otherNPCS.AddRange(GameObject.FindGameObjectsWithTag("NPC"));
        otherNPCS.Remove(gameObject);
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
            if (finalScores[i] < bestScore && !visitedNodes.Contains(targetNode.GetComponent<Node>().nodes[i]) && !visitedNodes.Contains(node[i]))
            {
                bestScore = finalScores[i];
                bestNode = node[i];
                Debug.Log(bestNode);
            }
            if (i == targetNode.GetComponent<Node>().nodes.Count - 1)
            {
                if (bestNode != null)
                {
                    Debug.Log("Best Node is: " + bestNode);
                    visitedNodes.Add(targetNode);
                    targetNode = bestNode;
                    Debug.Log("Best node is: " + bestNode);
                }
                else
                {
                    visitedNodes.Clear();
                    GetAllNodeDistance();
                    GetCloseNode();
                    targetNode = closestNode;
                }
            }
        }


        Debug.Log("Ran GetBestNode");

    }

    public void PlushChase()
    {
        if(!hasPlush)
        {
            for (int i = 0;i < otherNPCS.Count; i++)
            {
                if (!Physics2D.Linecast(transform.position, otherNPCS[i].transform.position, LayerMask.GetMask("Solid")))
                {
                    if (otherNPCS[i].GetComponent<NPC>().hasPlush)
                    {
                        if(distToTN <= 1f)
                        {
                            target.transform.position = otherNPCS[i].transform.position;
                        }
                        if (Vector2.Distance(transform.position, otherNPCS[i].transform.position) <= 1 && otherNPCS[i].GetComponent<NPC>().stealImmunity <= 0)
                        {
                            hasPlush = true;
                            otherNPCS[i].GetComponent<NPC>().hasPlush = false;
                            stealImmunity = 1f;
                        }
                    }
                }
            }

            if (!Physics.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Solid")))
            {
                if (player.GetComponent<PlayerStateManager>().hasPlush)
                {
                    if (distToTN <= 1f)
                    {
                        target.transform.position = player.transform.position;
                    }
                    if (Vector2.Distance(transform.position, player.transform.position) <= 1 && player.GetComponent<PlayerStateManager>().stealImmunity <= 0 && player.GetComponent<PlayerStateManager>().playerEquippedState == 5)
                    {
                        hasPlush = true;
                        player.GetComponent<PlayerStateManager>().hasPlush = false;
                        player.GetComponent<PlayerStateManager>().playerEquippedState = 0;
                        stealImmunity = 1f;
                    }
                }
            }
        } 
    }
}


/* The following code is code for resetting the nodes, helpful for preventing an NPC from getting stuck
 * 
 *          visitedNodes.Clear();
            GetAllNodeDistance();
            GetCloseNode();
            targetNode = closestNode;
*/ 
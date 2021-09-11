using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTestScript : MonoBehaviour
{

    public GameObject nodeMapManager;

    public GameObject currentNode;
    public GameObject endNode;
    private GameObject destinationNode;

    private NavMeshAgent monsterNavMeshAgent;

    public bool hasArrived = false;
    // Start is called before the first frame update
    void Start()
    {
        monsterNavMeshAgent = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        //If the monster hasn't arrived at its end destination yet, and it isn't moving (i.e. resting in a node), calculate a path to its destination.
        if(!hasArrived && monsterNavMeshAgent.velocity == Vector3.zero)
        {
            List<GameObject> pathList = calculatePathList();

            GameObject nextNode = pathList[pathList.Count - 1];
            //print(nextNode);
            monsterNavMeshAgent.SetDestination(nextNode.transform.position);
            print((Vector3.Distance(this.transform.position, nextNode.transform.position)));

            if ((Vector3.Distance(this.transform.position, nextNode.transform.position) <= 5*monsterNavMeshAgent.stoppingDistance)) // && nextNode != endNode)
            {
               // print((Vector3.Distance(this.transform.position, nextNode.transform.position)));
                currentNode = nextNode;
            }


        }
        //If the monster has arrived at its destination node, set the boolean appropriately.
        if (Vector3.Distance(this.transform.position, endNode.transform.position) <= 5*monsterNavMeshAgent.stoppingDistance)
        {
            hasArrived = true;
        }
        /*
        if (!hasArrived)
        {
            
            foreach(GameObject node in pathList)
            {
                print("NODE: " + node);
            }
            //print(destinationNode);
            //monsterNavMeshAgent.SetDestination(destinationNode.transform.position);

        }
        if (monsterNavMeshAgent.velocity == Vector3.zero)
        { 
            
            
        }
        */

    }

    List<GameObject> calculatePathList()
    {
        List<GameObject> ResultpathList = nodeMapManager.GetComponent<nodeLineManager>().FindShortestPathList(currentNode, endNode);


        return (ResultpathList);
    }

    public void setNewDestination(GameObject node)
    {
        endNode = node;
        hasArrived = false;
    }
}

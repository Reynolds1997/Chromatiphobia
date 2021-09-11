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

        if (!hasArrived)
        {
            List<GameObject> pathList = nodeMapManager.GetComponent<nodeLineManager>().FindShortestPathList(currentNode, endNode);
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

    }
}

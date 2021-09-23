using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class nodeLineManager : MonoBehaviour
{

    public GameObject currentlySelectedNode;
    public GameObject[] nodes;
    public List<Vector3> edges;
    public Color edgeColor;
    public Color blockColor;

    private List<Vector3> nodePositions;
    IDictionary<GameObject, GameObject> nodeParents = new Dictionary<GameObject, GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        nodePositions = new List<Vector3>();

        nodes = GameObject.FindGameObjectsWithTag("movementNode");

        foreach(GameObject node in nodes)
        {
            foreach(GameObject edgeConnection in node.GetComponent<nodeScript>().connectedNodes)
            {
                
            }

            nodePositions.Add(node.transform.position);

        }

    }

    // Update is called once per frame
    void Update()
    {
        //If a node is currently selected, draw the lines leading to it from other nodes.
        if(currentlySelectedNode!= null)
        {
            bool hasScout = currentlySelectedNode.GetComponent<nodeScript>().hasScout;

            foreach(GameObject node in nodes)
            {
                node.GetComponent<LineRenderer>().enabled = false;
                node.GetComponent<nodeScript>().barricadeSphere.GetComponent<MeshRenderer>().enabled = false;
                node.GetComponent<nodeScript>().viewCylinder.GetComponent<MeshRenderer>().enabled = false;
            }
            foreach (GameObject node in currentlySelectedNode.GetComponent<nodeScript>().connectedNodes)
            {

                node.GetComponent<nodeScript>().barricadeSphere.GetComponent<MeshRenderer>().enabled = true;
                
                node.GetComponent<LineRenderer>().enabled = true;
                node.GetComponent<nodeScript>().viewCylinder.GetComponent<MeshRenderer>().enabled = true;
                
                node.GetComponent<nodeScript>().DrawLine(currentlySelectedNode.transform.position, node.transform.position, edgeColor, edgeColor, hasScout);
            }
            foreach(GameObject node in currentlySelectedNode.GetComponent<nodeScript>().blockedNodes)
            {
                node.GetComponent<nodeScript>().barricadeSphere.GetComponent<MeshRenderer>().enabled = true;
                node.GetComponent<nodeScript>().DrawLine(currentlySelectedNode.transform.position, node.transform.position, blockColor, blockColor, hasScout);
            }
        }



        




    }
    
    public GameObject FindShortestPathBFS(GameObject startNode, GameObject goalNode)
    {
        //IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();
      //  IDictionary<GameObject, GameObject> nodeParents = new Dictionary<GameObject, GameObject>();

        Queue<GameObject> queue = new Queue<GameObject>();
        HashSet<GameObject> exploredNodes = new HashSet<GameObject>();
        queue.Enqueue(startNode);


        while(queue.Count!= 0)
        {
            GameObject currentNode = queue.Dequeue();
            if(currentNode.transform.position == goalNode.transform.position)
            {
                return currentNode;
            }

            List<GameObject> nodes = GetWalkableNodes(currentNode);

            foreach(GameObject node in nodes)
            {
                if (!exploredNodes.Contains(node))
                {

                    exploredNodes.Add(node);
                    nodeParents.Add(node, currentNode);
                    queue.Enqueue(node);
                }
            }
        }
        return startNode;

    }

    List<GameObject> GetWalkableNodes(GameObject node)
    {
        List<GameObject> resultList = new List<GameObject>();
        foreach(GameObject linkedNode in node.GetComponent<nodeScript>().connectedNodes)
        {
            resultList.Add(linkedNode);
        }

        return resultList;
    }

    //Returns a list of nodes between the monster's current position and their target position. 
    public List<GameObject> FindShortestPathList(GameObject node, GameObject endNode)
    {
        List<GameObject> nodePath = new List<GameObject>();

        GameObject goal;

       
        nodeParents.Clear();
        goal = FindShortestPathBFS(node, endNode);
        if(goal == node)
        {
            return null;
        }


        
        GameObject curr = goal;
        while (curr != node)
        {
            nodePath.Add(curr);
            curr = nodeParents[curr];
        }

        return nodePath;



    }

}

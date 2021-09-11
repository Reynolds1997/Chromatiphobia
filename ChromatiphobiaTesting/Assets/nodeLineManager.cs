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

    private List<Vector3> nodePositions;
    


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
        if(currentlySelectedNode!= null)
        {
            foreach(GameObject node in nodes)
            {
                node.GetComponent<LineRenderer>().enabled = false;
                node.GetComponent<nodeScript>().viewCylinder.GetComponent<MeshRenderer>().enabled = false;
            }
            foreach (GameObject node in currentlySelectedNode.GetComponent<nodeScript>().connectedNodes)
            {
                node.GetComponent<LineRenderer>().enabled = true;
                node.GetComponent<nodeScript>().viewCylinder.GetComponent<MeshRenderer>().enabled = true;

                node.GetComponent<nodeScript>().DrawLine(currentlySelectedNode.transform.position, node.transform.position, edgeColor, edgeColor);
            }
        }



        




    }
    /*
    Vector3 findShortestPathDjikstra(Vector3 startPosition, Vector3 goalPosition)
    {
        HashSet<Vector3> unexploredNodes = new HashSet<Vector3>();

        IDictionary<Vector3, int> distances = new Dictionary<Vector3, int>();
        IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();

        IEnumerable<Vector3> validNodes = nodePositions;

        foreach(Vector3 vertex in validNodes)
        {
            distances.Add(new KeyValuePair<Vector3, int>(vertex, int.MaxValue));
            nodeParents.Add(new KeyValuePair<Vector3, Vector3>(vertex, new Vector3(-1, -1, -1)));
            unexploredNodes.Add(vertex);
        }

        distances[startPosition] = 0;

        while (unexploredNodes.Count > 0)
        {
            Vector3 curr = distances.Where(x => unexploredNodes.Contains(x.Key)).OrderBy(x => x.Value).First().Key;

            if (curr == goalPosition)
            {
                print("Djikstra: " + distances[goalPosition]);
                return goalPosition;
            }

            unexploredNodes.Remove(curr);

            IList<Vector3> nodes = GetValidNodes(curr);

            foreach(Vector3 nodePosition in nodes)
            {
                int dist = distances[curr] + Weight(node);

                if(dist < distances[node])
                {
                    distances[node] = dist;
                    nodeParents[node] = curr;
                }

            }
        }

        return startPosition;


    }
    */

}

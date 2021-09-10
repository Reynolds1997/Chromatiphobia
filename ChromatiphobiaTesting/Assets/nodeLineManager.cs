using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeLineManager : MonoBehaviour
{

    public GameObject currentlySelectedNode;
    public GameObject[] nodes;
    // Start is called before the first frame update
    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("movementNode");
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

                node.GetComponent<nodeScript>().DrawLine(currentlySelectedNode.transform.position, node.transform.position, Color.green, Color.green);
            }
        }
        




    }



}

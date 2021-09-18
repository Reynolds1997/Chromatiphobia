using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphPathfinder : MonoBehaviour
{
    public string movementNodeTag = "movementNode";
    public GameObject[] nodes;
    // Start is called before the first frame update
    void Start()
    {
       nodes  = GameObject.FindGameObjectsWithTag("movementNode");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void getShortestPath(GameObject start, GameObject end)
    {

        if(start == null || end == null)
        {
            print("ERROR: NULL NODE GIVEN");
        }



        foreach(GameObject node in nodes)
        {
            
        }
    }
}

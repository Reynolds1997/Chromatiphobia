using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeScript : MonoBehaviour
{
    public bool isSelected;
    public int barricades = 2;
    public string barricadeTag = "barricade";
    private GameObject currentNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isSelected = this.gameObject.GetComponent<unitMovementScript>().isSelected;
        currentNode = this.gameObject.GetComponent<unitMovementScript>().currentNode;


        if (Input.GetMouseButtonDown(0) && isSelected)
        {
            //print("CLICKING");

            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit))
            {
                if (myRaycastHit.transform != null)
                {
                    GameObject hitObject = myRaycastHit.transform.gameObject;
                    print(hitObject);

                    if(hitObject != this.gameObject && hitObject.CompareTag(barricadeTag))
                    {
                        print("BARRICADE NODE CLICKED");
                        GameObject parentNode = hitObject.transform.parent.gameObject;

                        if (parentNode.GetComponent<nodeScript>().blockedNodes.Contains(currentNode))
                        {
                            removeBarricade(currentNode,parentNode);
                        }
                        else
                        {
                            barricadeConnection(currentNode, parentNode);
                        }

                    }
                }
            }
        }
    }


    void barricadeConnection(GameObject currentNode, GameObject endNode)
    {
        if(barricades > 0)
        {
            endNode.GetComponent<nodeScript>().connectedNodes.Remove(currentNode);
            endNode.GetComponent<nodeScript>().blockedNodes.Add(currentNode);

            currentNode.GetComponent<nodeScript>().connectedNodes.Remove(endNode);
            currentNode.GetComponent<nodeScript>().blockedNodes.Add(endNode);
            //barricades--;
        }
        
    }



    void removeBarricade(GameObject currentNode, GameObject endNode)
    {
        endNode.GetComponent<nodeScript>().connectedNodes.Add(currentNode);
        endNode.GetComponent<nodeScript>().blockedNodes.Remove(currentNode);

        currentNode.GetComponent<nodeScript>().connectedNodes.Add(endNode);
        currentNode.GetComponent<nodeScript>().blockedNodes.Remove(endNode);
        //barricades++;
    }
}

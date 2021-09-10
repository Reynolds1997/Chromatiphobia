using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class unitMovementScript : MonoBehaviour
{
    // ########################################################################3
    public GameObject selectionRing;
    public string moveNodeTag = "movementNode";
    public string playerUnitTag = "playerUnit";
    public GameObject currentNode;
    public NavMeshAgent unitNavMeshAgent;
    public Camera playerCamera;
    public bool isSelected = false;
    public float lineWidth = 1f;

    public GameObject nodeMapManager;

    public GameObject previousNode;
    public bool isBetweenNodes = false;

    public int indexNumber;
    public GameObject unitManager;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        nodeMapManager = GameObject.Find("NodeMapManager");
        unitNavMeshAgent = this.GetComponent<NavMeshAgent>();
        currentNode.GetComponent<nodeScript>().addUnit(this.gameObject);
        previousNode = currentNode;
    }

    // Update is called once per frame
    void Update()
    {

        //If there's a click, and the ship is selected, set a new destination.
        if (Input.GetMouseButtonDown(1) && isSelected)
        {
            //Unity cast a ray from the position of mouse cursor on screen toward the 3D scene
            Ray myRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit))
            {
                if (myRaycastHit.transform != null)
                {
                    GameObject hitObject = myRaycastHit.transform.gameObject;

                    //print(hitObject);
                    if (hitObject.CompareTag(moveNodeTag))
                    {
                      //  print("MOVE NODE CHECK: OK");
                        if (hitObject.GetComponent<nodeScript>().currentCapacity < hitObject.GetComponent<nodeScript>().maxCapacity)
                        {
                          //  print("CAPACITY CHECK: OK");
                            if (hitObject.GetComponent<nodeScript>().connectedNodes.Contains(currentNode))
                            {

                                if(hitObject == previousNode || unitNavMeshAgent.velocity == Vector3.zero)
                                {
                                    // print("REACHABLE CHECK: OK");
                                    unitNavMeshAgent.SetDestination(myRaycastHit.point);
                                    currentNode.GetComponent<nodeScript>().removeUnit(this.gameObject);
                                    previousNode = currentNode;
                                    currentNode = hitObject;
                                    hitObject.GetComponent<nodeScript>().addUnit(this.gameObject);
                                    nodeMapManager.GetComponent<nodeLineManager>().currentlySelectedNode = currentNode;
                                }
                            }
                        }
                    }
                }
            }

        }

        

        //If there's a click, and the unit is selected, check if the click is on the unit.
        //If the click is on a different unit, deselect this unit. 
        if (Input.GetMouseButtonDown(0) && isSelected)
        {

            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit))
            {
                if (myRaycastHit.transform != null)
                {

                    GameObject hitObject = myRaycastHit.transform.gameObject;
                    //print(hitObject);

                    //If the object is not this, and the object selected is a player ship, and shift is not held down, deselect this ship.
                    if (hitObject != this.gameObject && hitObject.CompareTag(playerUnitTag) && !Input.GetButton("Shift"))
                    {
                        deselectUnit();
                        print(this.name + " deselected");
                    }
                }
            }
        }



        if (isSelected)
        {
            //currentNode.Selected = true;
        }

        

    }


  
    public void toggleSelection()
    {
        if (isSelected)
        {
            deselectUnit();
        }
        else
        {
            selectUnit();
        }
    }


    public void selectUnit()
    {
        //if (this.gameObject.GetComponent<shipStatsManagerScript>().currentHealth > 0)
       // {
        isSelected = true;
        print("IS SELECTED");
        nodeMapManager.GetComponent<nodeLineManager>().currentlySelectedNode = currentNode;
        playerCamera.GetComponent<unitCameraFollowScript>().currentUnit = this.gameObject;

            //this.GetComponent<Outline>().enabled = true;

            selectionRing.GetComponent<MeshRenderer>().enabled = true;
           // fleetManagerObject.GetComponent<fleetManagerScript>().AddShipToSelection(this.gameObject);

        //}
    }
    public void deselectUnit()
    {
        isSelected = false;
        //this.GetComponent<Outline>().enabled = false;
        selectionRing.GetComponent<MeshRenderer>().enabled = false;
        //fleetManagerObject.GetComponent<fleetManagerScript>().RemoveShipFromSelection(this.gameObject);

    }

    


    void DrawLine(Vector3 start, Vector3 end, Color startColor, Color endColor)
    {
        LineRenderer lineRenderer = this.GetComponent<LineRenderer>(); // new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, start); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, end);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(moveNodeTag))
        {
            isBetweenNodes = true;
        }
    }


}



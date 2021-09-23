using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamouflageScript : MonoBehaviour
{
    private unitMovementScript unitScript;
    public string moveNodeTag = "movementNode";
    public Material camouflageMat;
    public int camoCapacity = 1;

    public int camoUnits = 4;
    // Start is called before the first frame update
    void Start()
    {
        unitScript = this.GetComponent<unitMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //If there's a click, and the ship is selected, set a new destination.
        if (Input.GetMouseButtonDown(0) && unitScript.isSelected)
        {
            //Unity cast a ray from the position of mouse cursor on screen toward the 3D scene
            Ray myRay = unitScript.playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit))
            {
                if (myRaycastHit.transform != null)
                {
                    GameObject hitObject = myRaycastHit.transform.gameObject;

                    //print(hitObject);
                    if (hitObject.CompareTag(moveNodeTag))
                    {

                        if(hitObject == unitScript.currentNode || unitScript.currentNode.GetComponent<nodeScript>().connectedNodes.Contains(hitObject)) {
                            nodeScript objectNodeScript = hitObject.GetComponent<nodeScript>();
                            if (objectNodeScript.camouflageCapacity <= 0 && camoUnits > 0)
                            {
                                objectNodeScript.camouflageCapacity = camoCapacity;
                                //objectNodeScript.trueMat = camouflageMat;
                                objectNodeScript.originalMat = camouflageMat;
                                objectNodeScript.nodeModel.gameObject.GetComponent<MeshRenderer>().material = camouflageMat;
                                camoUnits--;
                            }
                        }
                        
                    }

                }
            }
        }
    }
}

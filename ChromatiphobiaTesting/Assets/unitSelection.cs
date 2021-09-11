using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitSelection : MonoBehaviour
{

    private string playerUnitTag = "playerUnit";
    private string movementNodeTag = "movementNode";
    

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Unity cast a ray from the position of mouse cursor on screen toward the 3D scene
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit))
            {
                if (myRaycastHit.transform != null)
                {

                    GameObject hitObject = myRaycastHit.transform.gameObject;
                    printName(hitObject);
                    if (hitObject.CompareTag(playerUnitTag))
                    {
                        //if (Input.GetButton("Shift"))
                      //  {
                      //      hitObject.GetComponent<unitMovementScript>().toggleSelection();
                      //  }
                      //  else
                      //  {
                            hitObject.GetComponent<unitMovementScript>().selectUnit();
                     //   }

                        //print(hitObject.name + " selected");
                    }

                    //if (hitObject.CompareTag(movementNodeTag))
                    //{ 
                   //     hitObject.GetComponent<nodeScript>().debugOn = true;
                    //}
                    
                }
            }
        }
    }

    private void printName(GameObject gameObj)
    {
        print(gameObj.name);
    }



}

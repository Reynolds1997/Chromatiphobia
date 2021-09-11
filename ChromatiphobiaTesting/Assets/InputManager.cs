using System.Collections;
using System.Collections.Generic;
using UnityEngine;



 
 
public class InputManager : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject playerUnitManager;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        playerUnitManager = GameObject.Find("UnitManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerCamera.GetComponent<unitCameraFollowScript>().MoveHorizontal(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerCamera.GetComponent<unitCameraFollowScript>().MoveHorizontal(false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            playerCamera.GetComponent<unitCameraFollowScript>().rotateAroundUnit = true;

        }
        else
        {
            playerCamera.GetComponent<unitCameraFollowScript>().rotateAroundUnit = false;
        }


        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                playerUnitManager.GetComponent<UnitManager>().selectUnit(i);
                int numberPressed = i + 1;
                Debug.Log(numberPressed);
            }
        }
    }
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };
}

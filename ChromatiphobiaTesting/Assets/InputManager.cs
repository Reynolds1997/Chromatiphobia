using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
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
    }
}

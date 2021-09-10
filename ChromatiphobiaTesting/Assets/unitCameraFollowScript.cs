using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitCameraFollowScript : MonoBehaviour
{
    public GameObject currentUnit;

    
    public float smoothFactor = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private Vector3 offset = new Vector3(0, 15, -10);

    public float horizMove = 45f;
    public float vertMove = 15f;

    public bool lookAtUnit = true;
    public bool rotateAroundUnit = true;
    public float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - currentUnit.transform.position;
    }

    // LateUpdate is called after update methods
    void LateUpdate()
    {
        if (rotateAroundUnit)
        { 
         
            Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offset = cameraTurnAngle * offset;
        }

        Vector3 newPos = currentUnit.transform.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        if(lookAtUnit || rotateAroundUnit)
        {
            transform.LookAt(currentUnit.transform.position);
        }


        // Define a target position above and behind the target transform
        //Vector3 targetPosition = currentUnit.transform.position + offset;

        // Smoothly move the camera towards that target position
       // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //transform.LookAt(currentUnit.transform.position);

    }

    public void MoveHorizontal(bool left)
    {
        float dir = 1;
        if (!left)
        {
            dir *= -1;
        }

        Vector3 targetRotationPosition = new Vector3(currentUnit.transform.position.x, transform.position.y, currentUnit.transform.position.z);
        transform.RotateAround(currentUnit.transform.position, Vector3.up, dir*horizMove);
    }


}

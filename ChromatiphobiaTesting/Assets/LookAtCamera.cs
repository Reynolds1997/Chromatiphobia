using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Leave empty to look at main camera")]
    private GameObject Target = null;

    private Vector3 TargetPosition;

    public GameObject attachedObject = null;
    public float barOffset = 2f;

    public float xRotate = 0;
    public float yRotate = 0;
    public float zRotate = 0;

    void Start()
    {
        if (Target == null)
        {
            Target = Camera.main.gameObject;
        }
    }


    void Update()
    {
        if (attachedObject == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
            transform.rotation *= Quaternion.Euler(xRotate, yRotate, zRotate);
            transform.position = attachedObject.transform.position;
            transform.position += new Vector3(0, barOffset, 0);
        }

    }
}

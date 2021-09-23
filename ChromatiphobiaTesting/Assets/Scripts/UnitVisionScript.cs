using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisionScript : MonoBehaviour
{
    public AudioClip alertSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            if (alertSound != null)
            {
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(alertSound);
            }
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

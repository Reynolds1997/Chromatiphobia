using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitStatsManager : MonoBehaviour
{

    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed;

    public GameObject visionZone;
    public float visionZoneScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        this.gameObject.GetComponent<NavMeshAgent>().speed = moveSpeed;
        visionZone.transform.localScale = new Vector3(visionZoneScale, 1, visionZoneScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

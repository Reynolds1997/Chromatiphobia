using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitStatsManager : MonoBehaviour
{

    public int maxHealth = 10;
    public int currentHealth;
    public float standardMoveSpeed;
    public float currentMoveSpeed;

    public GameObject visionZone;
    public float standardVisionZoneScale = 1;
    public float currentVisionZoneScale;

    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMoveSpeed = standardMoveSpeed;
        currentVisionZoneScale = standardVisionZoneScale;
        this.gameObject.GetComponent<NavMeshAgent>().speed = currentMoveSpeed;
        visionZone.transform.localScale = new Vector3(currentVisionZoneScale, 1, currentVisionZoneScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Add health to the unit
    public void addHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //Take health away from the unit
    public void subtractHealth(int healthAmount)
    {
        currentHealth -= healthAmount;
        if (currentHealth <= 0)
        {
            killUnit();
        }
    }

    //Kill the unit
    private void killUnit()
    {
        isAlive = false;
        //Do other stuff to kill unit. Instantiate a dead body, destroy this unit, etc.
    }

    //Set a new scale multiplier for the unit's vision zone
    public void setVisionScale(int newVisionScale)
    {
        currentVisionZoneScale = newVisionScale;
        visionZone.transform.localScale = new Vector3(currentVisionZoneScale, 1, currentVisionZoneScale);

    }

    //Set a new movement speed for the unit
    public void setMoveSpeed(float newMoveSpeed)
    {
        currentMoveSpeed = newMoveSpeed;
        this.gameObject.GetComponent<NavMeshAgent>().speed = currentMoveSpeed;
    }


}

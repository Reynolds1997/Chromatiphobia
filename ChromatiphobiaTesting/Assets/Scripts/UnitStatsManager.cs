using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    public Image healthBar;


    public string unitName = "Scout";

    //HazadousNode Helper variables
    private bool onHazadousNode;
    private int frameRateCounter;
    private int secondsForDamage;
    private int damageOnHaz;
    private bool continousOROnceDelayedDamage;


    // Start is called before the first frame update
    void Start()
    {
        onHazadousNode = false;
        damageOnHaz = 0;
        frameRateCounter = 0;
        currentHealth = maxHealth;
        currentMoveSpeed = standardMoveSpeed;
        currentVisionZoneScale = standardVisionZoneScale;
        this.gameObject.GetComponent<NavMeshAgent>().speed = currentMoveSpeed;
        visionZone.transform.localScale = new Vector3(currentVisionZoneScale, 1, currentVisionZoneScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (onHazadousNode)
        {
            
            if(frameRateCounter == 240*secondsForDamage)
            {
                currentHealth -= damageOnHaz;
                if (continousOROnceDelayedDamage == false)
                {
                    onHazadousNode = false;
                }
                if(currentHealth == 0)
                {
                    killUnit();
                }
                frameRateCounter = 0;
            }
            else
            {
                frameRateCounter++;
            }
        }
        else
        {
            damageOnHaz = 0;
            frameRateCounter = 0;
        }
    }

    public void onHazNodeStart(bool start, int seconds, bool typeOfDelay, int damagePerHit)
    {
        onHazadousNode = start;
        secondsForDamage = seconds;
        continousOROnceDelayedDamage = typeOfDelay;
        damageOnHaz = damagePerHit;
    }

    public void onHazNodeEnd(bool end)
    {
        onHazadousNode = end;
        frameRateCounter = 0;
    }

    //Add health to the unit
    public void addHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateHealthBar();
    }

    //Take health away from the unit
    public void subtractHealth(int healthAmount)
    {
        currentHealth -= healthAmount;
        if (currentHealth <= 0)
        {
            killUnit();
        }
        updateHealthBar();
    }

    private void updateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    //Kill the unit
    private void killUnit()
    {
        isAlive = false;
        this.GetComponent<unitMovementScript>().isAlive = isAlive;
        print("UNIT KILLED!");
        this.gameObject.transform.position = this.transform.position + new Vector3(0, -1, 0); //Put the  unit partially in the ground.
        this.gameObject.SetActive(false);
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

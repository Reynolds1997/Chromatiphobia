using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneScript : MonoBehaviour
{

    public int damageAmount = 4;
    public float damageCooldown = 3;
    public float lastHitTime;
    // Start is called before the first frame update
    void Start()
    {
        lastHitTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("playerUnit"))
        {
            bool canBeDamaged = true;
            if(other.gameObject.GetComponent<unitMovementScript>().currentNode.GetComponent<nodeScript>().camouflageCapacity > 0)
            {
                int camoCapacity = other.gameObject.GetComponent<unitMovementScript>().currentNode.GetComponent<nodeScript>().camouflageCapacity;
                int currentCapacity = other.gameObject.GetComponent<unitMovementScript>().currentNode.GetComponent<nodeScript>().currentCapacity;

                if (currentCapacity <= camoCapacity)
                {
                    canBeDamaged = false;
                }
            }

            if (canBeDamaged)
            {
                dealDamage(other.gameObject);
            }
            
        }
    }

    private void dealDamage(GameObject target)
    {

        if (Time.time > damageCooldown + lastHitTime)
        {
            lastHitTime = Time.time;
            target.GetComponent<UnitStatsManager>().subtractHealth(damageAmount);
           
        }

        
    }

}

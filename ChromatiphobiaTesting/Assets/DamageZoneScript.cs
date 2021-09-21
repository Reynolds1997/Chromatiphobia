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
            dealDamage(other.gameObject);
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

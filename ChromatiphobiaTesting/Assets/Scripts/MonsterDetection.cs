using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetection : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    private bool isChasing = false;
    private bool playerDist1;
    private bool playerDist2;
    private bool playerDist3;
    private Vector3 playerpos;
    List<GameObject> foundUnits = new List<GameObject>();

    public GameObject damageSphere;

    public bool hasPlayedSound;
    public AudioClip alertSound;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerpos = new Vector3(1, 1, 1);
        damageSphere.SetActive(false);

    }

    void OnTriggerEnter(Collider other)
    {
        //Checks if there is a collider with the collider in front of the enemy,
        //checks which player unit collided, sets the agent's destination to the
        //player unit's position, and adds the player unit to a list.

        if (other.GetComponent<Collider>().tag == "playerUnit")
        {
            if (other.gameObject.name == "PlayerUnit1_Scout")
            {
                isChasing = true;
                playerpos = player1.transform.position;
                agent.SetDestination(playerpos);
                foundUnits.Add(player1);
            }

            if (other.gameObject.name == "PlayerUnit2_Sentry")
            {
                isChasing = true;
                playerpos = player2.transform.position;
                agent.SetDestination(playerpos);
                foundUnits.Add(player2);
            }

            if (other.gameObject.name == "PlayerUnit3_Engineer")
            {
                isChasing = true;
                playerpos = player3.transform.position;
                agent.SetDestination(playerpos);
                foundUnits.Add(player3);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Checks to see if the original player that collided is still
        //within the collider, and if not, it removes the unit
        //from the list of the detected player units.

        if (other.GetComponent<Collider>().tag == "playerUnit")
        {
            if (other.gameObject.name == "PlayerUnit1_Scout")
            {
                isChasing = false;
                foundUnits.Remove(player1);
            }

            if (other.gameObject.name == "PlayerUnit2_Sentry")
            {
                isChasing = false;
                foundUnits.Remove(player2);
            }

            if (other.gameObject.name == "PlayerUnit3_Engineer")
            {
                isChasing = false;
                foundUnits.Remove(player3);
            }
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        //If the isChasing bool is set to true and there are player units
        //in the list of found units, the agent's destination is constantly
        //set to the found player unit's position. It also deactivates the 
        //Patrolling script so the agent does not have two set destinations.

        if (isChasing)
        {
            
            
            if (foundUnits.Count > 0)
            {
                bool pursueFirstTarget = true;

                //If the first target's current node has a camouflage capacity, 
                if (foundUnits[0].GetComponent<unitMovementScript>().currentNode.GetComponent<nodeScript>().camouflageCapacity > 0)
                {
                    int camoCapacity = foundUnits[0].GetComponent<unitMovementScript>().currentNode.GetComponent<nodeScript>().camouflageCapacity;
                    int currentCapacity = foundUnits[0].GetComponent<unitMovementScript>().currentNode.GetComponent<nodeScript>().currentCapacity;

                    if(currentCapacity <= camoCapacity)
                    {
                        pursueFirstTarget = false;
                    }
                }


                if (!hasPlayedSound)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(alertSound);
                    hasPlayedSound = true;
                }

                if(foundUnits[0].GetComponent<UnitStatsManager>().currentHealth <= 0)
                {
                    pursueFirstTarget = false;
                    isChasing = false;
                    foundUnits.Remove(foundUnits[0]);
                }
                

                if (pursueFirstTarget)
                {
                    damageSphere.SetActive(true);
                    //print("PURSUING TARGET!");
                    GetComponent<Patrolling1>().enabled = false;
                    agent.destination = foundUnits[0].transform.position;
                    agent.speed = 2;
                }
                
            }
        }

        //If the isChasing bool is set back to false, the Patrolling script
        //is reenabled so the enemy goes back to its original path.

        else
        {
            hasPlayedSound = false;
            damageSphere.SetActive(false);
            GetComponent<Patrolling1>().enabled = true;
        }
    }
}

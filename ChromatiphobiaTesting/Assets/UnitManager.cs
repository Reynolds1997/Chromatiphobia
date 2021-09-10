using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitManager : MonoBehaviour
{

    public GameObject[] playerUnits;

    public GameObject currentlySelectedUnit;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject unit in playerUnits)
        {
            unit.GetComponent<unitMovementScript>().indexNumber = Array.IndexOf(playerUnits,unit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectUnit(int unitNumber)
    {
        deselectUnits();
        playerUnits[unitNumber].GetComponent<unitMovementScript>().selectUnit();
        currentlySelectedUnit = playerUnits[unitNumber];
    }

    public void deselectUnits()
    {
        foreach(GameObject unit in playerUnits)
        {
            unit.GetComponent<unitMovementScript>().deselectUnit();
        }
        //if (currentlySelectedUnit != null)
       // {
        //    currentlySelectedUnit.GetComponent<unitMovementScript>().deselectUnit();
        //}
    }
}

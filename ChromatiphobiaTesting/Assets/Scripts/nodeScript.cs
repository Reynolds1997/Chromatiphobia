using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nodeScript : MonoBehaviour
{

    public List<GameObject> blockedNodes;

    public List<GameObject> connectedNodes;
    public List<GameObject> currentOccupants;
    public int maxCapacity;
    public int currentCapacity;


    //node type properties variables
    public int visitsUntilDestroyed;
    public int damage;
    private Color startColor = Color.blue;
    private Color endColor = Color.green;
   

    public bool debugOn = false;

    public float lineWidth = 0.5f;

    public bool drawLines = false;

    public bool isSelected;

    public List<LineRenderer> lineRenderers;

    public string nodeName = "Standard";

    public TMPro.TMP_Text textLabel;

    public GameObject barricadeSphere;

    public GameObject viewCylinder;

    public bool hasScout = false;

    public int victoryCapacity = 10; //Minimum number of units required to win.

    public int camouflageCapacity = -1; //Maximum number of units that can hide in a node. Exceeding this count leaves all units in the node vulnerable.

    public Material trueMat;
    private Material originalMat;
    public GameObject nodeModel;


    // Start is called before the first frame update
    void Start()
    {
       originalMat = nodeModel.GetComponent<MeshRenderer>().material;
       if(trueMat == null)
       {
           trueMat = originalMat;
       }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (debugOn)
        {
           // DrawLines();
            
           // foreach(GameObject node in connectedNodes)
           // {
                
           //     node.GetComponent<nodeScript>().DrawLine(node.transform.position, this.transform.position, startColor,endColor);
           // }
            
        }
        
        /*
        else
        {
            foreach (GameObject node in connectedNodes)
            {
                node.GetComponent<LineRenderer>().enabled = false;
            }
        }
        */
        
        
    }

    public void addUnit(GameObject unit)
    {

        if(visitsUntilDestroyed != -1)
        {
            visitsUntilDestroyed--;
        }
        if(damage > 0) {
            unit.GetComponent<UnitStatsManager>().subtractHealth(damage);
            print(unit.GetComponent<UnitStatsManager>().currentHealth);
        }

        currentCapacity++;
        currentOccupants.Add(unit);
        if(currentCapacity >= victoryCapacity)
        {
            victory();
        }

        if(unit.GetComponent<UnitStatsManager>().unitName == "Scout")
        {
            hasScout = true;
        }
        UpdateText();

    }

    public void removeUnit(GameObject unit)
    {

        if (unit.GetComponent<UnitStatsManager>().unitName == "Scout")
        {
           // nodeModel.gameObject.GetComponent<MeshRenderer>().material = originalMat;
           // hasScout = false;
        }

        currentCapacity--;
        currentOccupants.Remove(unit);
        UpdateText();

        if (visitsUntilDestroyed == 0)
        {
            foreach (GameObject node in connectedNodes)
            {
               
                foreach(GameObject node1 in node.GetComponent<nodeScript>().connectedNodes)
                {
                    node1.GetComponent<nodeScript>().connectedNodes.Remove(this.gameObject);
                    
                }
            }
            Destroy(this.gameObject);
        }

        //print(unit.UnitStatsManager.currentHealth);
        //unit.currentHealth -= damage;

    }

    void UpdateText()
    {
        string newText = "";


        //    
        if (hasScout)
        {
            newText = nodeName + "\n" + currentCapacity.ToString() + "/" + maxCapacity.ToString();
        }
        else
        {
            newText = "Room" + "\n" + currentCapacity.ToString() + "/" + maxCapacity.ToString();
        }
        textLabel.SetText(newText);
    }

    public void DrawConnections()
    {
        foreach (GameObject node in connectedNodes)
        {
            node.GetComponent<nodeScript>().DrawLine(node.transform.position, this.transform.position, startColor, endColor,hasScout);
        }
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
      //  Debug.Log("Mouse is over GameObject.");
        debugOn = true;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
    //    Debug.Log("Mouse is no longer on GameObject.");
        debugOn = false;
    }

    public void DrawLine(Vector3 start, Vector3 end, Color startColor, Color endColor, bool hasScout)
    {


        //barricadeSphere.transform.position = (start + end) / 2;
        //barricadeSphere.GetComponent<MeshRenderer>().material.color = startColor;

        if (hasScout)
        {
            nodeModel.gameObject.GetComponent<MeshRenderer>().material = trueMat;
        }

        LineRenderer lineRenderer = this.GetComponent<LineRenderer>(); // new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.enabled = true;


        //lineRenderer.material.renderQueue = 1;

        lineRenderer.sortingOrder = 1;

       // lineRenderer.material = new Material(Shader.Find("Shaders/LineShader"));
        //lineRenderer.material.color = Color.green;

        // lineRenderer.material = new Material(Shader.Find("Shaders/LineShader"));
        lineRenderer.material.color = startColor;

        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));


        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, start); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, end);
    }

    void DrawLines()
    {
        LineRenderer lineRenderer = this.GetComponent<LineRenderer>(); // new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;


        List<Vector3> positions = new List<Vector3>();
        List<Vector3> finalPositions = new List<Vector3>();

        foreach (GameObject node in connectedNodes)
        {
            positions.Add(this.transform.position);
            positions.Add(node.transform.position);
            
        }
        

        lineRenderer.SetPositions(positions.ToArray()); //x,y and z position of the starting point of the line
    }


    private void OnDrawGizmos()
    {
        foreach(GameObject node in connectedNodes)
        {
            Gizmos.DrawLine(this.transform.position, node.transform.position);
        }
        
    }

    void victory()
    {
        print("VICTORY!");
        SceneManager.LoadScene(0);
    }
}

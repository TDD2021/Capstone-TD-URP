using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCheckerScript : MonoBehaviour
{
    private bool obstructed = false;
    private Transform parent;
    public GameData gameData;
    private bool m_Started;
    private Vector3 offsetPosition;

    public bool Obstructed => obstructed;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.transform;
        parent.localScale = new Vector3(gameData.CellSize, gameData.CellSize, gameData.CellSize);
        obstructed = false;
        m_Started = true;
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void FixedUpdate()
    {
        MyCollisions();
    }

    void MyCollisions()
    {
        obstructed = false;
        offsetPosition = gameObject.transform.parent.transform.position + (new Vector3(gameData.CellSize / 2, 0, gameData.CellSize / 2));
        Collider[] hitColliders = Physics.OverlapBox(offsetPosition, transform.parent.transform.localScale / 2);
        int i = 0;
        while(i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                //Debug.Log("Hit: " + hitColliders[i].name + "   Tag: " + hitColliders[i].gameObject.tag + " " + i);
                obstructed = true;
                break;
            }
            
            i++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
        {
            Gizmos.DrawWireCube(offsetPosition, transform.parent.transform.localScale);
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            obstructed = true;
        }
    }*/

    /*private void OnTriggerStay(Collider other)
    {
        obstructed = other.gameObject.tag == "Enemy";
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            obstructed = false;
        }
    }*/
}

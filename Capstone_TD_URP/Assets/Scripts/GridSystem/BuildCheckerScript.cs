using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCheckerScript : MonoBehaviour
{
    private bool obstructed = true;
    private Transform parent;
    public GameData gameData;

    public bool Obstructed => obstructed;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.transform;
        parent.localScale = new Vector3(gameData.CellSize, 0, gameData.CellSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            obstructed = false;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            obstructed = true;
        }
    }
}

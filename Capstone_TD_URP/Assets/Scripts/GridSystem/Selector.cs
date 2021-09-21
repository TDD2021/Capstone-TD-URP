using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public GameObject pathCheckReferee;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int tempPath = 0;
        if (pathCheckReferee.gameObject.GetComponent<PathChecker>().CheckPath())
        {
            tempPath = 1;
        }
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetInt("CanPlace", tempPath);
    }

    private void OnEnable()
    {
        int tempPath = 0;
        if (pathCheckReferee.gameObject.GetComponent<PathChecker>().CheckPath())
        {
            tempPath = 1;
        }
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetInt("CanPlace", tempPath);
    }
}

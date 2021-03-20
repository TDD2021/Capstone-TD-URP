using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<CubeCoords> minionPath = new List<CubeCoords>();

    void Start()
    {
        StartCoroutine(MoveMinion());
    }

    IEnumerator MoveMinion()
    {
        foreach (CubeCoords step in minionPath)
        {
            Vector3 cubeCoords = new Vector3(step.transform.position.x, 2.5f, step.transform.position.z);
            transform.position = cubeCoords;
            yield return new WaitForSeconds(2);
        }
    }

}

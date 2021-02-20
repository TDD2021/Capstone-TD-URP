using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.name + "--Collided with --" + collision.gameObject.name);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} **Entered Range of: ** {other.gameObject.name}");
        other.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    /*private void OnTriggerStay(Collider other)
    {
        Debug.Log($"{this.name} **In Range of: ** {other.gameObject.name}");
    }*/

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{this.name} **Left Range of: ** {other.gameObject.name}");
        other.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}

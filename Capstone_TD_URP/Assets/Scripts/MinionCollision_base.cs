using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCollision_base : MonoBehaviour
{


    private void OnCollisionEnter(Collision TheColliderThatIWillBeCollidingWith)
    {
        //Debug.Log(TheColliderThatIWillBeCollidingWith.gameObject.name);
        
       // if (TheColliderThatIWillBeCollidingWith.gameObject.CompareTag("Enemy"))
        //{
            //Destroy(TheColliderThatIWillBeCollidingWith.gameObject);
        Destroy(TheColliderThatIWillBeCollidingWith.gameObject);

       // }
        
    }

    }


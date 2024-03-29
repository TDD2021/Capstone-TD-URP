﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GatlingGun : MonoBehaviour
{
    [SerializeField]
    private TowerData towerData;

    // target the gun will aim at
    private Transform go_target;

    // Gameobjects need to control rotation and aiming
    public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;

    // Gun barrel rotation
    public float barrelRotationSpeed;
    float currentRotationSpeed;

    // Distance the turret can aim and fire from
    public float firingRange;

    // Particle system for the muzzel flash
    public ParticleSystem muzzelFlash;

    // Used to start and stop the turret firing
    bool canFire = false;

    //How long the enemy stay in collision 
    public float timerCountDown = 3.0f;
    // Is the player currently at location
    bool isPlayerColliding = false;

    //game object for Bullet
    public GameObject BulletPrefab;
    public Transform firePoint;

    public float fireRate = 5f;
    private float fireCountdown;


    //Turret damage 
    public float turretDamage = 10f;


    //shoot effect
    public GameObject impactEffect;

    //Display txt
    public Text goldGained;
    private int destroyedAmount = 0;

    void Start()
    {
        // Set the firing range distance
        // this.GetComponent<SphereCollider>().radius = firingRange;

        // Get firing range from data
        firingRange = towerData.Range;



        // Set the firing range distance
        this.GetComponent<SphereCollider>().radius = firingRange;





    }

    void Update()
    {
        AimAndFire();

        /*
         if (isPlayerColliding == true)
         {
           timerCountDown -= Time.deltaTime;
            if (timerCountDown < 0)
            {
                 timerCountDown = 0;
         }
        }
        */

    }

    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }


    // Detect an Enemy, aim and fire
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Entered Trigger");
            go_target = other.transform;
            canFire = true;
            //isPlayerColliding = true;



        }

    }



    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy in Trigger");
            go_target = other.transform;
            canFire = true;

        }
    }



    // Stop firing

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Left Trigger");
            canFire = false;
        }
    }

    void AimAndFire()
    {
        // Gun barrel rotation
        go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        // if can fire turret activates
        if (canFire == true && go_target != null)
        {
            // start rotation
            currentRotationSpeed = barrelRotationSpeed;

            // aim at enemy
            Vector3 baseTargetPostition = new Vector3(go_target.position.x, this.transform.position.y, go_target.position.z);
            Vector3 gunBodyTargetPostition = new Vector3(go_target.position.x, go_target.position.y, go_target.position.z);

            go_baseRotation.transform.LookAt(baseTargetPostition);
            go_GunBody.transform.LookAt(gunBodyTargetPostition);



            //Raycast to shoot
            RaycastHit hit;
            if (Physics.Raycast(go_baseRotation.position, go_baseRotation.transform.forward, out hit, 500f))
            {
                Debug.Log(hit.transform.name);

                MinionTarget target = hit.transform.GetComponent<MinionTarget>();

                if (target != null)
                {
                    target.takeDamage(turretDamage);
                }
                //impact effect to minion
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.4f);

                //Display gold get by destroying minion:
                //destroyedAmount++;

                //goldGained.text = destroyedAmount.ToString();
            }
            else
            {
                Debug.Log("Did not hit anything ");
            }
            //End of rayCast to Shoot


            // start particle system 


            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
            }

        }
        else
        {
            // slow down barrel rotation and stop
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);

            // stop the particle system
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
    }



    /*-----------Testing bullet system--------------*/

    /*

     void Shoot()
     {
         Debug.Log("SHoot");
        GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        BulletTower bullet = bulletGO.GetComponent<BulletTower>();
         if (bullet != null)
         {
             bullet.Seek(go_target);
         }
     }
    */
}
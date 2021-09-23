using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionCollision_base : MonoBehaviour
{
    private float health;
    public float startHealth = 100f;
    public Image healthBar;

    public GameObject impactEffect;

    Vector3 basePosition = new Vector3();


    private float damage = 10f;

    private void Start()
    {
        health = startHealth;
        basePosition = transform.position;
    }

    void OnCollisionEnter(Collision TheColliderThatIWillBeCollidingWith)
    {
        //Debug.Log(TheColliderThatIWillBeCollidingWith.gameObject.name);

        if (TheColliderThatIWillBeCollidingWith.gameObject.CompareTag("Enemy"))
        {

            Destroy(TheColliderThatIWillBeCollidingWith.gameObject);
            //Destroy(TheColliderThatIWillBeCollidingWith.gameObject);
            takeDamage(damage);


        }

    }

    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            GameObject impactGO = Instantiate(impactEffect, basePosition, Quaternion.LookRotation(basePosition));
            Destroy(impactGO, 0.4f);
            die();

        }
    }
    void die()
    {
        Destroy(gameObject);
        Time.timeScale = 0;
    }
}
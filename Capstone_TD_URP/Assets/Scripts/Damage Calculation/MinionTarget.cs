using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionTarget : MonoBehaviour
{
    //Minion Health 
    private float health;
    public float startHealth = 100f;

    //public Image healthBar;


    private void Start()
    {
        health = startHealth;
    }
    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;

        //healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
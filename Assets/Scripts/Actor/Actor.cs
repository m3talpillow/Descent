using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: Class for actor gameplay information and behavior
 * Authors: Jared Johannson
 */ 

public class Actor : MonoBehaviour, SuperMethods
{
    private float health { get; set; }
    private float stamina { get; set; }

	public void startInit ()
    {
        health = 100;
        stamina = 100;
	}

    public void awakeInit() { }

    public void takeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Debug.Log("You've died!");
    }

    public void useStamina(float staminaLoss)
    {
        stamina -= staminaLoss;

        if (stamina < 0)
            stamina = 0;
    }
}
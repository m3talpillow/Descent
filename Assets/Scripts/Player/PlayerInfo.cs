using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private float health;
    private float stamina;

	void Start ()
    {
        health = 100;
        stamina = 100;
	}
	
	void Update ()
    {
		
	}

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: Class that holds all information about all inheriting actors.
 *          These methods should not contain anything tied to an animation.
 * Authors: Jared Johannson
 */

[RequireComponent(typeof(Animator))]
public abstract class Actor : MonoBehaviour, IActorControl
{
    // Parent info
    protected Transform parent;

    // Temp attributes 
    private float max_health = 100;
    private float max_stamina = 100;
    protected bool dead;
    public float moveSpeed;
    private float health;
    private float stamina;
    public string characterName;
    

    /* Possible fields to add
     * 
     * private Stats[] stats;
     * private Armor[] armor;
     * private Buffs[] buffs;
     * private Inventory inventory;
     * private Equipment equipment;
     */

    // Animation fields
    protected Animator anim;
    protected int anim_vertical;
    protected int anim_horizontal;
    protected int anim_lightAttack;
    protected int anim_heavyAttack;
    protected int anim_drawWeapons;
    protected int anim_sheathWeapons;
    protected int anim_jump;
    protected int anim_mirror;
    protected int anim_armed;
    protected int anim_death;

    // Equiped items
    public GameObject weapon;


    /*   INITAILIZATION */

    // Called on every object in scene before anything else
    public virtual void Awake()
    {
        parent = transform.parent;
        if (parent == null)
        {
            Debug.Log("Actor " + characterName + " does not have parent object. Assigning to self.");
            parent = transform;
        }

        // Create hashed strings for faster Animator control
        anim_vertical = Animator.StringToHash("verticalMove");
        anim_horizontal = Animator.StringToHash("horizontalMove");
        anim_lightAttack = Animator.StringToHash("lightAttack");
        anim_heavyAttack = Animator.StringToHash("heavyAttack");
        anim_drawWeapons = Animator.StringToHash("drawSword");
        anim_sheathWeapons = Animator.StringToHash("sheathSword");
        anim_jump = Animator.StringToHash("jump");
        anim_mirror = Animator.StringToHash("mirror");
        anim_armed = Animator.StringToHash("armed");
        anim_death = Animator.StringToHash("death");

        // Set maxes
        health = max_health;
        stamina = max_stamina;
        moveSpeed = 0.1f;
        dead = false;
    }

    // Called when object is enabled, after all object activate awake
    protected void Start()
    {
        anim = this.GetComponent<Animator>();

        // Set all animation parameters to starting state
        anim.ResetTrigger(anim_lightAttack);
        anim.ResetTrigger(anim_heavyAttack);
        anim.ResetTrigger(anim_drawWeapons);
        anim.ResetTrigger(anim_sheathWeapons);
        anim.SetBool(anim_armed, false);
    }


    /*   UPDATES   */

    // Updates 30 times per second
    protected void FixedUpdate()
    {
        if (dead)
            return;

        RegainStamina();
        RegainHealth();
    }

    // Function to regulate how fast stamina is regained
    private void RegainStamina()
    {
        if (stamina < max_stamina)
        {
            stamina += max_stamina * 0.0005f;
            if (stamina > max_stamina)
                stamina = max_stamina;
        }
    }

    // Function to regulate how fast health is regained
    private void RegainHealth()
    {
        if (health < max_health)
        {
            health += max_health * 0.0005f;
            if (health > max_health)
                health = max_health;
        }
    }


    /*   TRIGGERABLE FUNCTIONS   */

    // All stamina lost goes through this function
    public void StaminaLost(float staminaLost)
    {
        stamina -= staminaLost;

        if (stamina < 0)
        {
            stamina = 0;
            Debug.Log("Stamina has run out for: " + characterName);
        }
    }

    // All damage taken goes through this function
    public void DamageTaken(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Death();
            Debug.Log("Health has run out for: " + characterName + ", and has died!");
            anim.SetTrigger(anim_death);
        }
    }

    // Move character fowards and backwards
    public virtual void MoveVertical(float verticalInput)
    {
        anim.SetFloat(anim_vertical, verticalInput);

        parent.position += parent.forward * verticalInput * moveSpeed;
    }

    // Move character left and right
    public virtual void MoveHorizontal(float horizontalInput)
    {
        anim.SetFloat(anim_horizontal, horizontalInput);

        parent.position += parent.right * horizontalInput * moveSpeed;
    }

    // Bring out weapons, now armed
    private void DrawWeapon()
    {
        anim.SetBool(anim_armed, true);
        anim.SetTrigger(anim_drawWeapons);
        anim.ResetTrigger(anim_sheathWeapons);
    }

    // Put weapons away, now unarmed
    private void SheathWeapon()
    {
        anim.SetBool(anim_armed, false);
        anim.SetTrigger(anim_sheathWeapons);
        anim.ResetTrigger(anim_drawWeapons);
    }

    // To draw or sheath your weapons
    public void ToggleWeapon()
    {
        if (weapon.activeSelf)
        {
            SheathWeapon();
            weapon.SetActive(false);
        }
        else
        {
            DrawWeapon();
            weapon.SetActive(true);
        }
    }

    // If player has weapon out, does light attack
    public virtual void LightAttack()
    {
        if (anim.GetBool(anim_armed))
            anim.SetTrigger(anim_lightAttack);

        StaminaLost(30);
    }

    // If player has weapon out, does heavy attack
    public virtual void HeavyAttack()
    {
        if (anim.GetBool(anim_armed))
            anim.SetTrigger(anim_heavyAttack);

        StaminaLost(45);
    }  

    public virtual void Jump()
    {
        anim.SetTrigger(anim_jump);
    }

    private void Death()
    {
        dead = true;
        InputManager.keyboardInputLocked = true;
    }
}
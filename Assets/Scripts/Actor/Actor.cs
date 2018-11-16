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

    // Character's current max
    private float max_health;
    private float max_stamina;

    // Character stats
    private float health;
    private float stamina;

    public string characterName;
    public float moveSpeed;

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
    protected int anim_mouseLeftClick;
    protected int anim_mouseRightClick;
    protected int anim_drawWeapons;
    protected int anim_sheathWeapons;
    protected int anim_armed;

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
        anim_vertical = Animator.StringToHash("Vertical");
        anim_horizontal = Animator.StringToHash("Horizontal");
        anim_mouseLeftClick = Animator.StringToHash("MouseLeftClick");
        anim_mouseRightClick = Animator.StringToHash("MouseRightClick");
        anim_drawWeapons = Animator.StringToHash("DrawSword");
        anim_sheathWeapons = Animator.StringToHash("SheathSword");
        anim_armed = Animator.StringToHash("Armed");

        // Set maxes
        max_health = 100;
        max_stamina = 100;
        moveSpeed = 0.1f;
    }

    // Called when object is enabled, after all object activate awake
    protected void Start()
    {
        anim = this.GetComponent<Animator>();

        // Set all animation parameters to starting state
        anim.ResetTrigger(anim_mouseLeftClick);
        anim.ResetTrigger(anim_mouseRightClick);
        anim.ResetTrigger(anim_drawWeapons);
        anim.ResetTrigger(anim_sheathWeapons);
        anim.SetBool(anim_armed, false);
    }


    /*   UPDATES   */

    // Updates 30 times per second
    protected void FixedUpdate()
    {
        RegainStamina();
        RegainHealth();
    }

    // Function to regulate how fast stamina is regained
    private void RegainStamina()
    {
        if (stamina < max_stamina)
        {
            stamina += stamina * 0.05f;
            if (stamina > max_stamina)
                stamina = max_stamina;
        }
    }

    // Function to regulate how fast health is regained
    private void RegainHealth()
    {
        if (health < max_health)
        {
            health += health * 0.05f;
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

        if (health < 0)
        {
            health = 0;
            Debug.Log("Health has run out for: " + characterName + ", and has died!");
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
    public virtual void DrawWeapon()
    {
        anim.SetBool(anim_armed, true);
        anim.SetTrigger(anim_drawWeapons);
        anim.ResetTrigger(anim_sheathWeapons);

        ToggleWeapon();
    }

    // Put weapons away, now unarmed
    public virtual void SheathWeapon()
    {
        anim.SetBool(anim_armed, false);
        anim.SetTrigger(anim_sheathWeapons);
        anim.ResetTrigger(anim_drawWeapons);

        ToggleWeapon();
    }

    // To draw or sheath your weapons
    private void ToggleWeapon()
    {
        if (weapon.activeSelf)
            weapon.SetActive(false);
        else
            weapon.SetActive(true);
    }

    // If player has weapon out, does light attack
    public virtual void LightAttack()
    {
        if (anim.GetBool(anim_armed))
            anim.SetTrigger(anim_mouseLeftClick);
    }

    // If player has weapon out, does heavy attack
    public virtual void HeavyAttack()
    {
        if (anim.GetBool(anim_armed))
            anim.SetTrigger(anim_mouseRightClick);
    }

    
}
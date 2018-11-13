using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: Abstract class to control actor animations & movement
 * Authors: Jared Johannson, Joe Peaden
 */ 

[RequireComponent(typeof(Animator))]
public abstract class ActorControl : MonoBehaviour, SuperMethods
{
	// Dependencies
    protected Equiped equiped;

    // Animation fields
    protected Animator anim;
    protected int anim_vertical;
    protected int anim_horizontal;
    protected int anim_mouseLeftClick;
    protected int anim_drawSword;
    protected int anim_sheathSword;
    protected int anim_armed;

    // Other
    protected Transform parent;
    public float moveSpeed;

    // On initialization gather dependencies 
    public void awakeInit()
    {
        anim = this.GetComponent<Animator>();
        equiped = this.GetComponent<Equiped>();
    }

    // for initialization of stuff in start method
    public void startInit()
    {
    	parent = transform.parent;

        // Create hashed strings for faster Animator control
        anim_vertical = Animator.StringToHash("Vertical");
        anim_horizontal = Animator.StringToHash("Horizontal");
        anim_mouseLeftClick = Animator.StringToHash("MouseLeftClick");
        anim_drawSword = Animator.StringToHash("DrawSword");
        anim_sheathSword = Animator.StringToHash("SheathSword");
        anim_armed = Animator.StringToHash("Armed");

        // Set all animation parameters to starting state
        anim.ResetTrigger(anim_mouseLeftClick);
        anim.ResetTrigger(anim_drawSword);
        anim.ResetTrigger(anim_sheathSword);
        anim.SetBool(anim_armed, false);
    }

        // Player takes out weapon
    protected void DrawWeapon()
    {
        // Animator controls
        anim.SetBool(anim_armed, true);
        anim.SetTrigger(anim_drawSword);
        anim.ResetTrigger(anim_sheathSword);

        equiped.togglePrimaryWeapon();

        InputManager.XKey -= DrawWeapon;
        InputManager.XKey += SheathWeapon;
    }

    // Player puts away weapon
    protected void SheathWeapon()
    {
        anim.SetBool(anim_armed, false);
        anim.SetTrigger(anim_sheathSword);
        anim.ResetTrigger(anim_drawSword);

        equiped.togglePrimaryWeapon();

        InputManager.XKey += DrawWeapon;
        InputManager.XKey -= SheathWeapon;
    }

    protected void LightAttack()
    {
        if (anim.GetBool(anim_armed))
            anim.SetTrigger(anim_mouseLeftClick);
    }

}
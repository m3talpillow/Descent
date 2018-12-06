﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerControl : MonoBehaviour
{
    // Dependencies
    private Equiped equiped;

    // Animation fields
    private Animator anim;
    private int anim_vertical;
    private int anim_horizontal;
    private int anim_mouseLeftClick;
    private int anim_drawSword;
    private int anim_sheathSword;
    private int anim_armed;

    private Transform parent;
    public float moveSpeed;
    public float rotateSpeed;

    // On initialization gather dependencies 
    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        equiped = this.GetComponent<Equiped>();
    }

    // Called after Awake() and once enabled
    void Start ()
    {
        // Subscribe to events from InputManager and assign methods
        InputManager.VerticalInput += MoveVertical;
        InputManager.HorizontalInput += MoveHorizontal;
        InputManager.RotationInput += CaptureRotation;
        InputManager.XKey += DrawWeapon;
        InputManager.LeftMouse += LightAttack;

        // Movement variables
        moveSpeed = 0.1f;
        rotateSpeed =  5.0f;
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

    // Capture mouse movement for camera rotation
    private void CaptureRotation(float rotationInput)
    {
        if(rotationInput > 0)
            parent.Rotate((Vector3.up) * rotateSpeed);
        else
            parent.Rotate((Vector3.down) * rotateSpeed);
    }

    // Move player forward and backwards
    private void MoveVertical(float verticalInput)
    {
        anim.SetFloat(anim_vertical, verticalInput);

        parent.position += parent.forward * verticalInput * moveSpeed;
    }

    // Move player left and right
    private void MoveHorizontal(float horizontalInput)
    {
        anim.SetFloat(anim_horizontal, horizontalInput);

        parent.position += parent.right * horizontalInput * moveSpeed;
    }

    // Player takes out weapon
    private void DrawWeapon()
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
    private void SheathWeapon()
    {
        anim.SetBool(anim_armed, false);
        anim.SetTrigger(anim_sheathSword);
        anim.ResetTrigger(anim_drawSword);

        equiped.togglePrimaryWeapon();

        InputManager.XKey += DrawWeapon;
        InputManager.XKey -= SheathWeapon;
    }

    private void LightAttack()
    {
        if (anim.GetBool(anim_armed))
            anim.SetTrigger(anim_mouseLeftClick);
    }
}

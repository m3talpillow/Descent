using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: Player implementation of ActorControl
 * Authors: Jared Johannson, Joe Peaden
 */ 

public class PlayerControl : ActorControl
{

    ////// Initialization //////

    void Awake ()
    {
        // call superclass initialization in awake function.
        // this is so PlayerControl still has the Awake function
        base.awakeInit();
    }

    void Start ()
    {
        base.startInit();

        // Subscribe to events from InputManager and assign methods
        InputManager.VerticalInput += MoveVertical;
        InputManager.HorizontalInput += MoveHorizontal;
        InputManager.XKey += DrawWeapon;
        InputManager.LeftMouse += LightAttack;

        // Movement variables
        moveSpeed = 0.1f;
    }

    ////// Movement //////

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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: Contains gameplay behavior specific to player
 * Authors: Jared Johannson
 */ 

public class Player : Actor
{
    public override void Awake()
    {
        base.Awake();
        characterName = "Player";

        // Subscribe to events from InputManager and assign methods
        InputManager.VerticalInput += MoveVertical;
        InputManager.HorizontalInput += MoveHorizontal;
        InputManager.XKey += DrawWeapon;
        InputManager.LeftMouse += LightAttack;
        InputManager.RightMouse += HeavyAttack;
    }

    public override void DrawWeapon()
    {
        base.DrawWeapon();

        InputManager.XKey -= DrawWeapon;
        InputManager.XKey += SheathWeapon;
    }

    public override void SheathWeapon()
    {
        base.SheathWeapon();

        InputManager.XKey -= SheathWeapon;
        InputManager.XKey += DrawWeapon;
    }
}

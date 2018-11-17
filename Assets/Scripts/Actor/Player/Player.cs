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
        characterName = "Player" + Object.FindObjectsOfType<Player>().Length.ToString();

        // Subscribe to events from InputManager and assign methods
        InputManager.VerticalMoveInput += MoveVertical;
        InputManager.HorizontalMoveInput += MoveHorizontal;
        InputManager.ToggleArmed += ToggleWeapon;
        InputManager.LightAttack += LightAttack;
        InputManager.HeavyAttack += HeavyAttack;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: Maps inputs to actions for the player.
 *          If deployed to additional platforms, easy to modify for actions.
 * Authors: Jared Johannson
 */

public class InputManager : MonoBehaviour
{
    public static bool inputLocked;
    public static bool mouseInputLocked;
    public static bool keyboardInputLocked;

    public static float verticalMouseInput;
    public static float horizontalMouseInput;

    /* Delegates are function variables. 
     * Assign a function to them to be used when it is called. 
     * This allows us to replace functions like we change variables.
     * In another class, the desired method is assigned to its reference function.
     */

    // Actions that passes along a float to the subscribed functions
    public delegate void InputAction(float inputValue);
    //public static event InputAction VerticalMouseInput;
    //public static event InputAction HorizontalMouseInput;
    public static event InputAction VerticalMoveInput;
    public static event InputAction HorizontalMoveInput;

    public delegate void TriggerAction();
    public static event TriggerAction Sprint;
    public static event TriggerAction Jump;
    public static event TriggerAction ToggleArmed;

    // Combat
    public static event TriggerAction LightAttack;
    public static event TriggerAction HeavyAttack;
    public static event TriggerAction Block;
    public static event TriggerAction Dodge;
    
    public void Awake()
    {
        inputLocked = false;
        mouseInputLocked = false;
        keyboardInputLocked = false;
    }

    private void Update()
    {
        if (inputLocked)
            return;

        if (!mouseInputLocked)
            MouseInputs();

        if (!keyboardInputLocked)
            KeyboardInputs();
    }

    private void MouseInputs()
    {
        verticalMouseInput = Input.GetAxis("Mouse Y");
        horizontalMouseInput = Input.GetAxis("Mouse X");

        if (Input.GetKeyDown(KeyCode.Mouse0) && LightAttack != null)
            LightAttack();

        if (Input.GetKeyDown(KeyCode.Mouse1) && HeavyAttack != null)
            HeavyAttack();
    }

    private void KeyboardInputs()
    {
        if (VerticalMoveInput != null)
            VerticalMoveInput(Input.GetAxis("Vertical"));

        if (HorizontalMoveInput != null)
            HorizontalMoveInput(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.X) && ToggleArmed != null)
            ToggleArmed();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Dodge != null) Dodge();
            if (Jump != null) Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Dodge != null) Block();
            if (Jump != null) Sprint();
        }
    }
}

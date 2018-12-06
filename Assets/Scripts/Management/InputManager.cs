using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: Maps inputs to actions for the player.
 *          If deployed to additional platforms, easy to modify for actions.
 * Authors: Jared Johannson
 */

public class InputManager : Singleton<InputManager>
{
    public static bool inputLocked;
    public static bool mouseInputLocked;
    public static bool keyboardInputLocked;

    public static float verticalMouseInput;
    public static float horizontalMouseInput;
    public static float scrollwheelInput;

    /* Delegates are function variables. 
     * Assign a function to them to be used when it is called. 
     * This allows us to replace functions like we change variables.
     * In another class, the desired method is assigned to its reference function.
     */
<<<<<<< Updated upstream
=======
    public delegate void MovementAction(float inputValue);
    // This is a field of the delegate, which is assigned a method to call in another class
    public static event MovementAction VerticalInput;
    public static event MovementAction HorizontalInput;
    public static event MovementAction RotationInput;
>>>>>>> Stashed changes

    // Actions that passes along a float to the subscribed functions
    public delegate void InputAction(float inputValue);
    //public static event InputAction VerticalMouseInput;
    //public static event InputAction HorizontalMouseInput;
    public static event InputAction VerticalMoveInput;
    public static event InputAction HorizontalMoveInput;
    public static event InputAction CameraZoom;

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
        scrollwheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKeyDown(KeyCode.Mouse0) && LightAttack != null)
            LightAttack();

        if (Input.GetKeyDown(KeyCode.Mouse1) && HeavyAttack != null)
            HeavyAttack();

<<<<<<< Updated upstream
        if (scrollwheelInput != 0 && CameraZoom != null)
            CameraZoom(scrollwheelInput);
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
=======
        // Rotation inputs
        if(Input.GetAxis("Mouse X") != 0 && RotationInput != null)
            RotationInput(Input.GetAxis("Mouse X"));
        
        // Key press inputs
        if (Input.anyKey)
>>>>>>> Stashed changes
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

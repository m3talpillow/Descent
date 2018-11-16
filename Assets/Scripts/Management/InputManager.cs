using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: Maps keys to actions for the player
 * Authors: Jared Johannson
 */

public class InputManager : MonoBehaviour
{
    public static bool inputLocked;

    /* Delegates are function variables. 
     * Assign a function to them to be used when it is called. 
     * This allows us to replace functions like we change variables.
     */
    public delegate void MovementAction(float inputValue);
    // This is a field of the delegate, which is assigned a method to call in another class
    public static event MovementAction VerticalInput;
    public static event MovementAction HorizontalInput;

    public delegate void KeyboardAction();
    public static event KeyboardAction XKey;
    public static event KeyboardAction LeftMouse;
    public static event KeyboardAction RightMouse;

    public void Awake()
    {
        inputLocked = false;
    }

    // If a change in value and the action is subscribed to, trigger event
    private void Update()
    {
        if (inputLocked)
            return; 

        // Movement inputs
        if (Input.GetAxis("Vertical") != 0 && VerticalInput != null)
            VerticalInput(Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0 && HorizontalInput != null)
            HorizontalInput(Input.GetAxis("Horizontal"));

        // Key press inputs
        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.X) && XKey != null)
                XKey();

            if (Input.GetKeyDown(KeyCode.Mouse0) && LeftMouse != null)
                LeftMouse();

            if (Input.GetKeyDown(KeyCode.Mouse1) && RightMouse != null)
                RightMouse();
        }
    }
}

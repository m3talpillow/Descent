using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /* Delegates are function variables. 
     * Assign a function to them to be used when it is called. 
     * This allows us to replace functions like we change variables.
     */
    public delegate void MovementAction(float inputValue);
    // This is a field of the delegate, which is assigned a method to call in another class
    public static event MovementAction VerticalInput;
    public static event MovementAction HorizontalInput;

    void Start()
    {

    }

    private void Update()
    {
        // If a change in value and the action is subscribed to, trigger event
        if (Input.GetAxis("Vertical") != 0 && VerticalInput != null)
            VerticalInput(Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0 && HorizontalInput != null)
            HorizontalInput(Input.GetAxis("Horizontal"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerInputScript : MonoBehaviour
{
    // Dependencies
    private Animator anim;

    // Fields controlled by class
    private float verticalInput;
    private float horizontalInput;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    void Start ()
    {
        anim.SetBool("Armed", false);
	}
	
	void Update ()
    {
        GetMovementInput();
	}

    private void GetMovementInput()
    {
        // Get WASD/ Arrow inputs
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // Set animator variables from inputs
        if (verticalInput != 0 || horizontalInput != 0)
            anim.SetBool("Moving", true);
        else
            anim.SetBool("Moving", false);


        anim.SetFloat("Vertical", verticalInput);
        anim.SetFloat("Horizontal", horizontalInput);

        // Get Mouse inputs
        if (Input.GetKeyDown(KeyCode.Mouse0))
            anim.SetTrigger("MouseLeftClick");

        // Get Keyboard inputs
        if (Input.GetKeyDown(KeyCode.X))
        {
            bool armed = anim.GetBool("Armed");

            if (armed)
            {
                anim.SetTrigger("SheathSword");
                anim.ResetTrigger("DrawSword");
            }
            else
            {
                anim.SetTrigger("DrawSword");
                anim.ResetTrigger("SheathSword");
            }

            // Toggle armed status
            anim.SetBool("Armed", !armed);
        }
    }
}

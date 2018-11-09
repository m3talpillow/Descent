using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerInputScript : MonoBehaviour
{
    // Dependencies
    private Animator anim;

    // Input fields
    private float verticalInput;
    private float horizontalInput;

    // Animation fields
    private int anim_vertical;
    private int anim_horizontal;
    private int anim_mouseLeftClick;
    private int anim_drawSword;
    private int anim_sheathSword;
    private int anim_moving;
    private int anim_armed;

    // On initialization
    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        anim_vertical = Animator.StringToHash("Vertical");
        anim_horizontal = Animator.StringToHash("Horizontal");
        anim_mouseLeftClick = Animator.StringToHash("MouseLeftClick");
        anim_drawSword = Animator.StringToHash("DrawSword");
        anim_sheathSword = Animator.StringToHash("SheathSword");
        anim_moving = Animator.StringToHash("Moving");
        anim_armed = Animator.StringToHash("Armed");
}

    // On enabled
    void Start ()
    {
        // Reset all animation parameters
        anim.ResetTrigger(anim_mouseLeftClick);
        anim.ResetTrigger(anim_drawSword);
        anim.ResetTrigger(anim_sheathSword);
        anim.SetBool(anim_armed, false);
        anim.SetBool(anim_moving, false);
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
            anim.SetBool(anim_moving, true);
        else
            anim.SetBool(anim_moving, false);


        anim.SetFloat(anim_vertical, verticalInput);
        anim.SetFloat(anim_horizontal, horizontalInput);

        // Get Mouse inputs
        if (Input.GetKeyDown(KeyCode.Mouse0))
            anim.SetTrigger(anim_mouseLeftClick);

        // Get Keyboard inputs
        if (Input.GetKeyDown(KeyCode.X))
        {
            bool armed = anim.GetBool(anim_armed);

            if (armed)
            {
                anim.SetTrigger(anim_sheathSword);
                anim.ResetTrigger(anim_drawSword);
            }
            else
            {
                anim.SetTrigger(anim_drawSword);
                anim.ResetTrigger(anim_sheathSword);
            }

            // Toggle armed status
            anim.SetBool(anim_armed, !armed);
        }
    }
}

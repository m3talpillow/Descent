using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    // Assign which functions to call when InputManager fires
	void Start()
    {
        InputManager.VerticalInput += MoveVertical;
        InputManager.HorizontalInput += MoveHorizontal;
    }

    private void MoveVertical(float verticalInput)
    {
        transform.position += transform.forward * verticalInput;
    }

    private void MoveHorizontal(float horizontalInput)
    {
        transform.position += transform.right * horizontalInput;
    }
}

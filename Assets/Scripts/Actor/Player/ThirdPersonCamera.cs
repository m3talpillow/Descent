using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Variables to decide on concrete values 
    public float maxVerticalRotation = 70f;
    public float minVerticalRotation = -50f;

    // Used for keeping track of camera movements
    private float verticalRotation;
    private float horizontalRotation;
    
    void Awake ()
    {
        // Find starting rotation of camera
        Vector3 rotation = transform.localRotation.eulerAngles;
        verticalRotation = rotation.x;
        horizontalRotation = rotation.y;

        // Hide and disable cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
    // Modifying Euler angles of camera object before assigning back
	void Update ()
    {
        // Use sensitivity to adjust how quick camera moves
        verticalRotation += -InputManager.verticalMouseInput * Time.deltaTime * Settings.sensitivity;
        horizontalRotation += InputManager.horizontalMouseInput * Time.deltaTime * Settings.sensitivity;

        // Restrict camera rotation
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);
        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0.0f);
    }
}

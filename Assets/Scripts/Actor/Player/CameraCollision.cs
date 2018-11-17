using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose: If camera comes in contact with an object or an object
 *          obscures camera view, camera moves towards player.
 * Authors: Jared Johannson
 */

public class CameraCollision : MonoBehaviour
{
    // Variables to decide on concrete values
    public float minDistance = 1f;
    public float maxDistance = 3.5f;
    public float smoothing = 20f;
    public float cushion = 0.9f;

    private float setDistance;
    private float adjustDistance;
    private Vector3 dollyDirection;

    // Get camera's position relative to CameraBase and the vector's length
    void Awake ()
    {
        dollyDirection = transform.localPosition.normalized;
        setDistance = transform.localPosition.magnitude;
	}
	
	// Find where camera should be, if a collision is detected then move camera forward
	void Update ()
    {
        Vector3 desiredPosition = transform.parent.TransformPoint(dollyDirection * setDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredPosition, out hit))
            adjustDistance = Mathf.Clamp(hit.distance * cushion, minDistance, maxDistance);
        else
            adjustDistance = setDistance;

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDirection * adjustDistance, Time.deltaTime * smoothing);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equiped : MonoBehaviour
{
    public GameObject primaryWeapon;
	
	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void togglePrimaryWeapon()
    {
        if (primaryWeapon.activeSelf)
            primaryWeapon.SetActive(false);
        else
            primaryWeapon.SetActive(true);
    }
}

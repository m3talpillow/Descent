using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static float sensitivity { get; set; }

	void Awake ()
    {
        sensitivity = 120f;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: Player implementation of ActorControl
 * Authors: Jared Johannson, Joe Peaden
 */ 

public class EnemyControl : ActorControl
{

    ////// Initialization //////

    void Awake ()
    {
        // call superclass initialization in awake function.
        // this is so PlayerControl still has the Awake function
        base.awakeInit();
    }

    void Start ()
    {
        base.startInit();

        // Movement variables
        moveSpeed = 3f;
    }

    ////// Movement //////

    public void engagePlayer(Transform target)
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}

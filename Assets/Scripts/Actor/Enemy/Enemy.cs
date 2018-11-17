using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: Contains gameplay behavior specific to enemy
 * Authors: Joe Peaden
 */

public class Enemy : Actor
{
    // whatever enemy is moving towards
    public GameObject target;

    public override void Awake()
    {
        base.Awake();
        characterName = "Enemy" + Object.FindObjectsOfType<Player>().Length.ToString(); ;
    }

    // move towards target
    private void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        float step = moveSpeed * Time.deltaTime;
        parent.position = Vector3.MoveTowards(parent.position, target.transform.position, step);
    }
}
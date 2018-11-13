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

	// movement controller for enemy
	public EnemyControl mvmntCntrl;

	private void Start()
	{
		base.startInit();
	}

	private void Update()
	{
		mvmntCntrl.engagePlayer(target.transform);
	}

}
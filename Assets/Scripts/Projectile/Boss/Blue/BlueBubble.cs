﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBubble : BossProjectile
{
    [SerializeField]
    private bool isStunt = false;
    [SerializeField]
    private float stuntDuration = 1.0f;
    protected override void Attack(GameObject target)
    {
        if (isStunt)
        {
            Debug.Log("Player stunt for " + stuntDuration);
            target.GetComponent<CharacterStats>().TakeDamage(damage);
            Execute();
        }
        else
        {
            target.GetComponent<CharacterStats>().TakeDamage(damage);
            Execute();
        }
    }
}
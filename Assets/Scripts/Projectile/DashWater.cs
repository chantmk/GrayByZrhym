﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWater : EnemyProjectile
{
    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }

    public void Clear()
    {
        duration = 0.0f;
    }
}

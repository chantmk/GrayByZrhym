﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossChase : BossBehaviour
{
    /**
     * This class work on chasing the player this will always listen if it have to attack
     */

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        bossMovement.Chase();
        ListenToAttackSignal();
        ListenToDashSignal();
        updateMovingAnimation();
    }

    private void updateMovingAnimation()
    {
        var direction = enemyRigidbody.velocity.normalized;
        animator.SetFloat(AnimatorParams.Horizontal, direction.x);
        animator.SetFloat(AnimatorParams.Vertical, direction.y);
    }

    private void ListenToDashSignal()
    {
        if (enemyMovement.IsReadyToDash())
        {
            animator.SetInteger(AnimatorParams.Movement, (int)MovementEnum.Roll);
        }
    }
}

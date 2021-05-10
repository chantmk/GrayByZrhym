﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float visionRange = 1.0f;
    public bool ManualFlip = false;
    [Header("Dash parameters")]
    [Range(0.0f, 1.0f)]
    public float DashProbability;
    public float DashRange;
    public float DashDuration;
    public float DashCooldown;
    [Header("Patrol parameters")]
    public Vector2[] MovePositionsOffset = new Vector2[1];

    protected int toSpot = 0;

    private Transform player;
    private Vector2 startPosition;
    private float dashDurationLeft;
    private float dashCooldownLeft;
    private bool isDashing = false;
    private Rigidbody2D enemyRigidbody;

    protected virtual void Start()
    {
        startPosition = transform.position;
        dashDurationLeft = DashDuration;
        dashCooldownLeft = DashCooldown;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    protected virtual void Update()
    {
        updateDash();
    }

    public Vector3 GetVectorToPlayer()
    {
        return player.position - transform.position;
    }

    public void FlipToPlayer()
    {
        float xDirection = GetVectorToPlayer().x;
        float enemyX = transform.localScale.x;
        if (xDirection < -0.01f)
        {
            enemyX = -Mathf.Abs(enemyX);
        }
        else if (xDirection > 0.01f)
        {
            enemyX = Mathf.Abs(enemyX);
        }
        transform.localScale = new Vector3(enemyX, transform.localScale.y, transform.localScale.z);
    }

    public void Patrol()
    {
        enemyRigidbody.velocity = (GetNextPatrolPosition() - transform.position).normalized * speed;
    }

    public virtual Vector3 GetNextPatrolPosition()
    {
        updateToSpot();
        if (toSpot == 0)
        {
            return startPosition;
        }
        else
        {
            return startPosition + MovePositionsOffset[toSpot-1];
        }
    }

    protected virtual void updateToSpot()
    {
        if (Vector3.Distance(transform.position, MovePositionsOffset[toSpot]) < 0.2f)
        {
            toSpot += 1;

            if (toSpot > MovePositionsOffset.Length)
            {
                toSpot = 0;
            }
        }
    }

    public bool IsReadyToDash()
    {
        float shouldDash = Random.Range(0.0f, 1.0f);
        return !isDashing && dashCooldownLeft <= 0.0f && shouldDash < DashProbability;
    }

    public void Dash()
    {
        isDashing = true;
        enemyRigidbody.velocity = GetVectorToPlayer().normalized * (DashRange/DashDuration);
    }

    private void updateDash()
    {
        if (!isDashing && dashCooldownLeft > 0.0f)
        {
            dashCooldownLeft -= Time.fixedDeltaTime;
        }

        if (isDashing && dashDurationLeft > 0.0f)
        {
            dashDurationLeft -= Time.fixedDeltaTime;
            Dash();
        }
        else if (dashDurationLeft <= 0.0f)
        {
            isDashing = false;
            dashDurationLeft = DashDuration;
            dashCooldownLeft = DashCooldown;
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }
    public void Chase()
    {
        enemyRigidbody.velocity = GetVectorToPlayer().normalized * speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DashRange);
    }
}

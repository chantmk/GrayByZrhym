﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHitbox : MonoBehaviour
{

    public bool IsHitboxActivate
    {
        get => attackCollider.isActiveAndEnabled;
    }

    private Collider2D attackCollider;
    
    private float activateDuration;
    public Action<Collider2D> OnHitboxTriggerEnter;
    public Action<Collider2D> OnHitboxTriggerExit;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnHitboxTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnHitboxTriggerExit?.Invoke(other);
    }

    public void Enable()
    {
        attackCollider.enabled = true;

    }

    public void Disable()
    {
        attackCollider.enabled = false;
    }

    public void QuickEnable()
    {
        EnableForDuration(0.1f);
    }

    public void EnableForDuration(float duration)
    {
        if (duration > 0)
        {
            activateDuration = duration;
            Enable();
        }
        else
        {
            throw new Exception("duration must greater than 0");
        }
    }

    private void FixedUpdate()
    {
        if (activateDuration > 0)
        {
            activateDuration -= Time.fixedDeltaTime;
            if (activateDuration <= 0)
            {
                activateDuration = 0;
                Disable();
            }
        }
        
    }
}


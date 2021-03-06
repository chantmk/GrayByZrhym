﻿using System;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Objects
{
    public class TileManager : MonoBehaviour
    {
        public enum TileStateEnum
        {
            Active,
            Decay,
            ShouldRemove
        }
        private StateMachine<TileStateEnum> stateMachine;
        
        public ColorEnum TileColor; 
        private SpriteRenderer spriteRenderer;
        

        public float LifeTimeMin;
        public float LifeTimeMax;
        private float LifeTime;

        private float MaxDecayLifeTime;
        private float DecayLifeTime;
        public bool shouldRemove => stateMachine == null ? false
            : stateMachine.CurrentState == TileStateEnum.ShouldRemove;

        private bool isTriggerStay;
        private string clearerTag;

        private void Awake()
        {
            stateMachine = new StateMachine<TileStateEnum>(TileStateEnum.Active);
        }

        private void Start()
        {
            
            spriteRenderer = GetComponent<SpriteRenderer>();
            LifeTime = Random.Range(LifeTimeMin, LifeTimeMax);

            MaxDecayLifeTime = 1f;
            DecayLifeTime = MaxDecayLifeTime;

            switch (TileColor)
            {
                case ColorEnum.Black:
                    clearerTag = "BlackTileClearer";
                    break;
                case ColorEnum.Blue:
                    clearerTag = "BlueTileClearer";
                    break;
                default:
                    clearerTag = "";
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (stateMachine.CurrentState == TileStateEnum.Active && 
                other.gameObject.CompareTag("Player"))
            {
                isTriggerStay = true;
            }



            if (other.gameObject.CompareTag(clearerTag))
            {
                stateMachine.SetNextState(TileStateEnum.ShouldRemove);
                stateMachine.ChangeState();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (stateMachine.CurrentState == TileStateEnum.Active && 
                other.gameObject.CompareTag("Player"))
            {
                isTriggerStay = false;
            }
        }

        private void ChangeAlpha()
        {
            var color = spriteRenderer.color;
            color.a = DecayLifeTime/MaxDecayLifeTime;
            spriteRenderer.color = color;
        }
        
        void FixedUpdate()
        {
            switch (stateMachine.CurrentState)
            {
                case TileStateEnum.Active:
                    if (LifeTime <= 0f)
                    {
                        stateMachine.SetNextState(TileStateEnum.Decay);
                    }
                    break;
                case TileStateEnum.Decay:
                    if (DecayLifeTime <= 0f)
                    {
                        stateMachine.SetNextState(TileStateEnum.ShouldRemove);
                    }

                    break;
            }
            stateMachine.ChangeState();

            switch (stateMachine.CurrentState)
            {
                case TileStateEnum.Active:
                    if (isTriggerStay)
                        EventPublisher.TriggerStepOnTile(TileColor);
                    LifeTime -= Time.fixedDeltaTime;
                    break;
                case TileStateEnum.Decay:
                    ChangeAlpha();
                    DecayLifeTime -= Time.fixedDeltaTime;
                    break;
            }
        }
    }
}


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
        private bool ShouldRemove;

        private void Start()
        {
            stateMachine = new StateMachine<TileStateEnum>(TileStateEnum.Active);
            
            spriteRenderer = GetComponent<SpriteRenderer>();
            LifeTime = Random.Range(LifeTimeMin, LifeTimeMax);

            MaxDecayLifeTime = 1f;
            DecayLifeTime = MaxDecayLifeTime;
            
            
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EventPublisher.TriggerStepOnTile(TileColor);
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
                    if (MaxDecayLifeTime <= 0f)
                    {
                        stateMachine.SetNextState(TileStateEnum.ShouldRemove);
                    }

                    break;
            }
            stateMachine.ChangeState();

            switch (stateMachine.CurrentState)
            {
                case TileStateEnum.Active:
                    LifeTime -= Time.fixedDeltaTime;
                    break;
                case TileStateEnum.Decay:
                    ChangeAlpha();
                    DecayLifeTime -= Time.fixedDeltaTime;
                    break;
                case TileStateEnum.ShouldRemove:
                    ShouldRemove = true;
                    break;
            }
        }
    }
}
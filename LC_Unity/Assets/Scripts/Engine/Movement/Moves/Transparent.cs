﻿using Field;
using UnityEngine;

namespace Engine.Movement.Moves
{
    public class Transparent : Move
    {
        public bool On { get; set; }

        public override void Run(Agent agent)
        {
            if(agent != null)
            {
                SpriteRenderer sr = agent.GetComponent<SpriteRenderer>();
                Color color = sr.color;
                color.a = On ? 0.0f : 1.0f;
                sr.color = color;

                Animator animator = agent.GetComponent<Animator>();
                if (animator)
                    animator.enabled = !On;

                Collider2D collider = agent.GetComponent<Collider2D>();
                if(collider)
                    collider.enabled = !On;

                IsFinished = true;
            }
        }
    }
}

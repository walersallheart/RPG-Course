using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName ="Heatlh Effect", menuName = "Abilities/Effects/Health", order = 0)]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] float healthChange;
        public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action finished)
        {
            foreach (var target in targets)
            {
                Health health = target.GetComponent<Health>();

                if (health){
                    if (healthChange < 0) {
                        health.TakeDamage(user, -healthChange);
                    } else {
                        health.Heal(healthChange);
                    }
                }
            }

            finished();
        }
    }
}

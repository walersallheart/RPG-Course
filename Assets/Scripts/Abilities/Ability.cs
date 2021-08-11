using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Abilities{
    [CreateAssetMenu(fileName ="My Ability", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilterStrategy[] filterStrategies;
        [SerializeField] EffectStrategy[] effectStrategies;

        public override void Use(GameObject user){
            AbilityData data = new AbilityData(user);

            targetingStrategy.StartTargeting(data, 
            () => {
                TargetAcquired(data);
            });
        }

        private void TargetAcquired(AbilityData data){
            Debug.Log("Target Acquired");

            foreach (var filterStrategy in filterStrategies)
            {
                data.SetTargets(filterStrategy.Filter(data.GetTargets()));
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished(){

        }
    }
}

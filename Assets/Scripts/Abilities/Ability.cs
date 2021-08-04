using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Abilities{
    [CreateAssetMenu(fileName ="My Ability", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;
        
        public override void Use(GameObject user){
            targetingStrategy.StartTargeting(user, TargetAcquired);
        }

        private void TargetAcquired(IEnumerable<GameObject> targets){
            Debug.Log("Target Acquired");

            foreach (var target in targets)
            {
                Debug.Log(target);
            }
        }
    }
}

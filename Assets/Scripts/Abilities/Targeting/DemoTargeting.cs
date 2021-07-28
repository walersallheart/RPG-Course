using UnityEngine;

namespace RPG.Abilities{
    [CreateAssetMenu(fileName ="Demo Targeting", menuName = "Abilities/Targeting/Demo", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting()
        {
            Debug.Log("Demo Targeting Started");
        }
    }
}
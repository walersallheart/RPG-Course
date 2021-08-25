using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float maxMana = 200f;

        float mana;

        private void Awake() {
            mana = maxMana;
        }

        public float GetMana(){
            return mana;
        }

        public float GetMaxMana(){
            return maxMana;
        }

        public bool UseMana(float manaToUse) {
            if (manaToUse > mana) {
                return false;
            }

            mana -= manaToUse;
            return true;
        }
    }
}

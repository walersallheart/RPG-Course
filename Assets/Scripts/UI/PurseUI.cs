using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using TMPro;
using UnityEngine;

namespace RPG.UI {
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI balanceField;

        Purse playerPurse = null;

        private void Start() {
            playerPurse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();

            RefreshUI();

            playerPurse.onChange += RefreshUI;
        }

        void RefreshUI(){
            balanceField.text = $"${playerPurse.GetBalance():N2}";
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using TMPro;
using UnityEngine;

namespace RPG.UI.Shops{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI shopName;
        [SerializeField] Transform listRoot;
        [SerializeField] RowUI rowPrefab;
        [SerializeField] TextMeshProUGUI totalField;

        Shopper shopper = null;
        Shop currentShop = null;

        void Start()
        {
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();

            if (shopper == null) {
                return;
            }

            shopper.activeShopChanged += ShopChanged;
            ShopChanged();
        }

        private void ShopChanged(){
            if (currentShop != null) {
                currentShop.onChange -= RefreshUI;
            }

            currentShop = shopper.GetActiveShop();

            gameObject.SetActive(currentShop != null);

            if (currentShop == null) { return; }

            shopName.text = currentShop.GetShopName();

            currentShop.onChange += RefreshUI;

            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (ShopItem item in currentShop.GetFilteredItems())
            {
                RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);
                row.Setup(currentShop, item);
            }

            totalField.text = $"Total: ${currentShop.TransactionTotal():N2}";
        }

        public void Close(){
            shopper.SetActiveShop(null);
        }
        public void ConfirmTransaction(){
            currentShop.ConfirmTransaction();
        }
    }
}
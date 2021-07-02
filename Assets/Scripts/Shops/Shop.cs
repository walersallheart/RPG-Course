using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using System;
using RPG.Control;
using RPG.Inventories;

namespace RPG.Shops{
    public class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;

        //Stock Config
        [SerializeField] StockItemConfig[] stockConfig;

        [System.Serializable]
        class StockItemConfig{
            public InventoryItem item;
            public int initialStock;
            [Range(0,100)]
            public float buyingDiscountPercentage;
        }

        Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
        Shopper currentShopper = null;

        public event Action onChange;

        public void SetShopper(Shopper shopper) {
            currentShopper = shopper;
        }

        public IEnumerable<ShopItem> GetFilteredItems(){ 
            return GetAllItems();
        }

        public IEnumerable<ShopItem> GetAllItems(){
            foreach (StockItemConfig config in stockConfig)
            {
                float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage/100);
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                yield return new ShopItem(config.item, config.initialStock, price, quantityInTransaction);
            }
        }

        public void SelectFilter(ItemCategory category){}
        public ItemCategory GetFilters(){ return ItemCategory.None; }
        public void SelectMode(bool isBuying) {}
        public bool IsBuyingMode() { return true; }
        public bool CanTransact() { return true; }
        public void ConfirmTransaction(){
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            Purse shopperPurse = currentShopper.GetComponent<Purse>();

            if (shopperInventory == null || shopperPurse == null) { return; }

            foreach (ShopItem shopItem in GetAllItems()) {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                float price = shopItem.GetPrice();

                for (int i = 0; i < quantity; i++)
                {
                    if (shopperPurse.GetBalance() < price) { break; }

                    bool success = shopperInventory.AddToFirstEmptySlot(item, 1);

                    if (success) {
                        AddToTransaction(item, -1);
                        shopperPurse.UpdateBalance(-price);
                    }
                }
            }
        }

        public string GetShopName()
        {
            return shopName;
        }

        public float TransactionTotal(){ 
            float total = 0;

            foreach (ShopItem item in GetAllItems())
            {
                total += item.GetPrice() * item.GetQuantityInTransaction();
            }

            return total;
        }
        public void AddToTransaction(InventoryItem item, int quantity) {
            if (!transaction.ContainsKey(item)) {
                transaction[item] = 0;
            }

            transaction[item] += quantity;

            if (transaction[item] <= 0) {
                transaction.Remove(item);
            }

            if (onChange != null) {
                onChange();
            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0)) {
                callingController.GetComponent<Shopper>().SetActiveShop(this);
            }

            return true;
        }
    }
}
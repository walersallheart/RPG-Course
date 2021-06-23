using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using System;
using RPG.Control;

namespace RPG.Shops{
    public class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;

        public event Action onChange;

        public IEnumerable<ShopItem> GetFilteredItems(){ 
            yield return new ShopItem(InventoryItem.GetFromID("e75a0c32-d41c-4651-8496-92cb958a8f1e"),
            10, 10.0f, 0);

            yield return new ShopItem(InventoryItem.GetFromID("28c6f2e6-46e9-4879-a14f-d6998c781cb7"),
            10, 10.0f, 0);

            yield return new ShopItem(InventoryItem.GetFromID("dbc1e40e-d3bd-4e26-a62b-6cff0e46c415"),
            10, 10.0f, 0);

            yield return new ShopItem(InventoryItem.GetFromID("77ad666c-513c-4c88-87b2-c8594bcd5a89"),
            10, 10.0f, 0);
        }
        public void SelectFilter(ItemCategory category){}
        public ItemCategory GetFilters(){ return ItemCategory.None; }
        public void SelectMode(bool isBuying) {}
        public bool IsBuyingMode() { return true; }
        public bool CanTransact() { return true; }
        public void ConfirmTransaction(){}

        public string GetShopName()
        {
            return shopName;
        }

        public float TransactionTotal(){ return 0;}
        public void AddToTransaction(InventoryItem item, int quantity) {}

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
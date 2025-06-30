using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace negProt.ShopSystem
{
   public class Shop : MonoBehaviour
   {
      [Header("Items List")]
      [SerializeField] private ShopItem[] shopItems;

      [Header("Refernces")] 
      [SerializeField] private Transform shopContainer;
      [SerializeField] private GameObject shopItemPrefab;

      private List<GameObject> shopItemPrefabs;
      private void Awake()
      {
         if(shopContainer==null)
            shopContainer = null;
         if (shopItemPrefab == null)
            shopItemPrefab = null;
         
         shopItemPrefabs = new List<GameObject>();
      }

      private void Start()
      {
         PopulateShop();
      }

      private void PopulateShop()
      {
         for (int i = 0; i < shopItems.Length; i++)
         {
            ShopItem item = shopItems[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);
            shopItemPrefabs.Add(itemObject);
            //ShopItemPrefab Structure
            //ShopItem(Image,Button)
            // - Border(Image)
            // - Sprite Image(Image)
            // - Name Text(TextmeshProUGUI)
            // - Cost Text(")
            
            itemObject.GetComponent<Button>().onClick.AddListener(()=>OnButtonClick(item));

            itemObject.GetComponent<Image>().color = item.itemBGColor;
            itemObject.transform.GetChild(1).GetComponent<Image>().sprite = item.itemSprite;
            itemObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.itemName;
            itemObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
               SaveManager.instance.IsPlayerOwned(i)
                  ? SaveManager.instance.CurrentlySelectedPlayer() == i ? "Selected!" : "Purchased"
                  : item.itemPrice.ToString();
         }
      }

      private void OnButtonClick(ShopItem item)
      {
         //Purchase Logic
         int index = System.Array.IndexOf(shopItems,item);
         if (!SaveManager.instance.IsPlayerOwned(index))
         {
            if (SaveManager.instance.BuyPlayer(index, (int)item.itemPrice))
            {
               //Select this as current player
               SaveManager.instance.SetCurrentPlayer(index);
            }
            else
            {
               //Feedback for Unable to buy!
            }   
         }
         else
         {
            //Select this as current player
            SaveManager.instance.SetCurrentPlayer(index);
         }

         for (int i = 0; i < shopItemPrefabs.Count; i++)
         {
            if (index == i)
            {
               if(SaveManager.instance.IsPlayerOwned(i))
                  shopItemPrefabs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Selected!";
               else
                  shopItemPrefabs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = item.itemPrice.ToString();
            }
            else
            { 
               //shopItemPrefabs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Purchased";
               if(SaveManager.instance.IsPlayerOwned(i))
                  shopItemPrefabs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Purchased";
               //else
                  //shopItemPrefabs[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = item.itemPrice.ToString();
            }
         }
         //Debug.Log("Item Name : " + item.itemName + "\nItem Cost : "+item.itemPrice);
      }
   }
   
}

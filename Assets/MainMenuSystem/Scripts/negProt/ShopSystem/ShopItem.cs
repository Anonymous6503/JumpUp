using UnityEngine;

namespace negProt.ShopSystem
{
    [CreateAssetMenu(fileName = "New Shop Item",menuName = "Shop/Shop Item")]
    public class ShopItem : ScriptableObject
    {
        //Add Border Image if it is dynamic
        //else declare it by default in the ShopItem Prefab
        public string itemName;
        public float itemPrice;
        
        public Sprite itemSprite;
        public Color itemBGColor;
        //public GameObject itemPrefab;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShopSystem
{
    [System.Serializable]
    public class ShopData 
    {
        public int cash;
        public int selectedIndex;
        public ShopItem[] shopItems;
    }
    [System.Serializable]
    public class ShopItem
    {
        
        public string ItemName;
        public bool isUnlocked;
        public int unlockCost;
        public int unlockedLevel;
        public CharacterInfo[] characterLevel;

    }
    [System.Serializable]
    public class CharacterInfo
    {
        public int unlockCost;
        public int speed;
        public int acceleration;

    }
}



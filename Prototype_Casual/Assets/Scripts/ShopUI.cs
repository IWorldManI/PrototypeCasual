using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class ShopUI : MonoBehaviour
    {
        public ShopData shopData;
        public GameObject[] characterModels;
        public Text unlockBtnText, upgradeBtnText, levelText, characterNameText;
        public Text speedText,accelerarionText,totalCoinsText;
        public Button unlockBtn, upgradeBtn, nextBtn, previousBtn;
        public PlayerController player;
        public SaveLoadData saveLoadData;

        public int priceOfTrash;

        private int currentIndex = 0;
        private int selectedIndex = 0;
        
        
        
        private void Start()
        {
            saveLoadData.Initialize();
            
            selectedIndex = shopData.selectedIndex;
            currentIndex = selectedIndex;
            totalCoinsText.text = "" + shopData.cash;

            player.dataIndex = currentIndex;
            player.SetSkin();

            SetCharacterInfo();

            unlockBtn.onClick.AddListener(()=>UnlockSelectBtnMethcod());
            upgradeBtn.onClick.AddListener(()=>UpgradeBtnMethcod());
            nextBtn.onClick.AddListener(()=>NextBtnMethcod());
            previousBtn.onClick.AddListener(()=>PreviousBtnMethcod());

            characterModels[currentIndex].SetActive(true);
            if (currentIndex == 0) previousBtn.interactable = false;
            if (currentIndex == shopData.shopItems.Length - 1) nextBtn.interactable = false;

            UnlockBtnStatus();
            UpgradeBtnStatus();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                shopData.cash += player.trash * priceOfTrash;
                totalCoinsText.text = "" + shopData.cash;
                player.trash = 0;
                player.trashTotal.text = "" + player.trash;
                saveLoadData.SaveData();
            }
            
            
        }
        public void SetCharacterInfo()
        {
            totalCoinsText.text = "" + shopData.cash;
            characterNameText.text = shopData.shopItems[currentIndex].ItemName;
            int currentLevel = shopData.shopItems[currentIndex].unlockedLevel;
            levelText.text = "Level:" + (currentLevel + 1);
            speedText.text = "Speed:" + shopData.shopItems[currentIndex].characterLevel[currentLevel].speed;
            accelerarionText.text = "Acc" + shopData.shopItems[currentIndex].characterLevel[currentLevel].acceleration;

            saveLoadData.SaveData();
        }
        private void NextBtnMethcod()
        {
            if (currentIndex < shopData.shopItems.Length - 1)
            {
                characterModels[currentIndex].SetActive(false);
                currentIndex++;
                characterModels[currentIndex].SetActive(true);
                SetCharacterInfo();

                if (currentIndex == shopData.shopItems.Length - 1) nextBtn.interactable = false;

                if (!previousBtn.interactable) previousBtn.interactable = true;

                UnlockBtnStatus();
                UpgradeBtnStatus();
            }
        }
        private void PreviousBtnMethcod()
        {
            if (currentIndex > 0)
            {
                characterModels[currentIndex].SetActive(false);
                currentIndex--;
                characterModels[currentIndex].SetActive(true);
                SetCharacterInfo();

                if (currentIndex == 0) previousBtn.interactable = false;

                if (!nextBtn.interactable) nextBtn.interactable = true;

                UnlockBtnStatus();
                UpgradeBtnStatus();
            }
        }
        private void UnlockSelectBtnMethcod()
        {
            bool yesSelected = false;
            if (shopData.shopItems[currentIndex].isUnlocked)
            {
                yesSelected = true;
            }
            else
            {
                if (shopData.cash >= shopData.shopItems[currentIndex].unlockCost)
                {
                    shopData.cash -= shopData.shopItems[currentIndex].unlockCost;
                    totalCoinsText.text = "" + shopData.cash;
                    yesSelected = true;
                    shopData.shopItems[currentIndex].isUnlocked=true;
                    UpgradeBtnStatus();
                    saveLoadData.SaveData();
                }
            }

            if(yesSelected)
            {
                unlockBtnText.text = "Selected";
                selectedIndex = currentIndex;
                shopData.selectedIndex = selectedIndex;
                unlockBtn.interactable = false;

                player.dataIndex = currentIndex;
                player.SetSkin();
                saveLoadData.SaveData();
            }
        }
        private void UpgradeBtnMethcod()
        {
            saveLoadData.SaveData();
            int nextLevelIndex = shopData.shopItems[currentIndex].unlockedLevel + 1;

            if (shopData.cash >= shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost)
            {
                shopData.cash -= shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost;
                totalCoinsText.text = "" + shopData.cash;
                shopData.shopItems[currentIndex].unlockedLevel++;

                if (shopData.shopItems[currentIndex].unlockedLevel < shopData.shopItems[currentIndex].characterLevel.Length - 1)
                {
                    upgradeBtnText.text = "UpgradeCost" + shopData.shopItems[currentIndex].characterLevel[nextLevelIndex + 1].unlockCost;
                }
                else
                {
                    upgradeBtn.interactable = false;
                    upgradeBtnText.text = "MaxLevelRached";
                }
                SetCharacterInfo();
            }
        }
        private void UnlockBtnStatus()
        {
            if (shopData.shopItems[currentIndex].isUnlocked)
            {
                unlockBtn.interactable = selectedIndex != currentIndex ? true : false;
                unlockBtnText.text = selectedIndex == currentIndex ? "Selected" : "Select";
            }
            else
            {
                unlockBtn.interactable = true;
                unlockBtnText.text = "Cost" + shopData.shopItems[currentIndex].unlockCost;
            }
        }
        private void UpgradeBtnStatus()
        {
            if ((shopData.shopItems[currentIndex].isUnlocked))
            {
                if (shopData.shopItems[currentIndex].unlockedLevel < shopData.shopItems[currentIndex].characterLevel.Length - 1)
                {
                    int nextLevelIndex = shopData.shopItems[currentIndex].unlockedLevel + 1;
                    upgradeBtn.interactable = true;
                    upgradeBtnText.text = "UpgradeCost" + shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost;
                }
                else
                {
                    upgradeBtn.interactable = false;
                    upgradeBtnText.text = "MaxLevelRached";
                }
            }
            else
            {
                upgradeBtn.interactable = false;
                upgradeBtnText.text = "Locked";
            }

        }

        
    }
    
}

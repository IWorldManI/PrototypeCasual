using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ShopSystem
{
    public class ShopUI : MonoBehaviour
    {
        public ShopData shopData;
        public GameObject[] characterModels;
        public TextMeshProUGUI unlockBtnText, upgradeBtnText, levelText, characterNameText;
        public TextMeshProUGUI speedText,accelerarionText,totalCoinsText;
        public Button unlockBtn, upgradeBtn, nextBtn, previousBtn;
        public PlayerController player;
        public SaveLoadData saveLoadData;
        public Shop_Point shopPoint;
        public PickupTrash pickupCircle;
        

        public int priceOfTrash;

        private int currentIndex = 0;
        private int selectedIndex = 0;

        public UI_Manager ui_manager;
        
        
        
        private void Start()
        {
            saveLoadData.Initialize();                                      //initialize save system
            
            selectedIndex = shopData.selectedIndex;
            currentIndex = selectedIndex;
            totalCoinsText.text = "<sprite=0> " + shopData.cash;

            player.dataIndex = currentIndex;
            player.SetSkin();                                               //activate current car model and disable other

            SetCharacterInfo();                                             //update coins,car settings, text coins and etc.

            unlockBtn.onClick.AddListener(()=>UnlockSelectBtnMethcod());    //connect buttons to functions 
            upgradeBtn.onClick.AddListener(()=>UpgradeBtnMethcod());
            nextBtn.onClick.AddListener(()=>NextBtnMethcod());
            previousBtn.onClick.AddListener(()=>PreviousBtnMethcod());

            characterModels[currentIndex].SetActive(true);                  //activate current model from shop
            if (currentIndex == 0) previousBtn.interactable = false;
            if (currentIndex == shopData.shopItems.Length - 1) nextBtn.interactable = false;

            UnlockBtnStatus();
            UpgradeBtnStatus();

        }

        private void OnTriggerEnter(Collider other)                         //there is a sale of boxes
        {
            if (other.gameObject.CompareTag("Player") && player.trash != 0)
            {
                ui_manager.AnimatedTextScaleCoin();                         //add animation to sells
                shopData.cash += player.trash * priceOfTrash;               //at what price we sell boxes, can be customized in the future
                totalCoinsText.text = "<sprite=0> " + shopData.cash;
                player.trash = 0;                                           
                player.trashTotal.text = "<sprite=1> " + player.trash;
                ui_manager.AnimatedTextScaleBox();                          //text animation for juiciness
                shopPoint.AnimationShop();                                  //shop model animation
                saveLoadData.SaveData();                                    //save player progress
            }
            else if (other.gameObject.CompareTag("NPC") && other.GetComponent<NpcPatrolSimple>().npcTrash != 0)  //npc sells boxes with animation (mb need fix)
            {
                shopData.cash += other.GetComponent<NpcPatrolSimple>().npcTrash * priceOfTrash;
                totalCoinsText.text = "<sprite=0> " + shopData.cash;
                other.GetComponent<NpcPatrolSimple>().npcTrash = 0;
                ui_manager.AnimatedTextScaleCoin();
                ui_manager.AnimatedTextScaleBox();
                shopPoint.AnimationShop();
                saveLoadData.SaveData();
            }
            
            
        }
        public void SetCharacterInfo()
        {
            totalCoinsText.text = "<sprite=0> " + shopData.cash;
            characterNameText.text = shopData.shopItems[currentIndex].ItemName;
            int currentLevel = shopData.shopItems[currentIndex].unlockedLevel;
            levelText.text = "Level: " + (currentLevel + 1);
            speedText.text = "Speed: " + shopData.shopItems[currentIndex].characterLevel[currentLevel].speed;
            accelerarionText.text = "Zone: " + shopData.shopItems[currentIndex].characterLevel[currentLevel].acceleration;
            if(player.dataIndex==currentIndex)
            {
                player.speed = shopData.shopItems[currentIndex].characterLevel[currentLevel].speed;                      //upgrade character speed (Fixed)
                player.maxSpeed = shopData.shopItems[currentIndex].characterLevel[currentLevel].speed;                   //upgrade character maxspeed (Fixed)
                pickupCircle.fieldOfPickup = shopData.shopItems[currentIndex].characterLevel[currentLevel].acceleration;
                pickupCircle.UpdateCircleRadius();                                                                       //acceleration = Circle radius
            }
            //player.speed = shopData.shopItems[selectedIndex].characterLevel[currentLevel].speed; //update info of speed car

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
                SetCharacterInfo(); //update 

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
                SetCharacterInfo(); //update
            }
            else
            {
                if (shopData.cash >= shopData.shopItems[currentIndex].unlockCost)
                {
                    shopData.cash -= shopData.shopItems[currentIndex].unlockCost;
                    totalCoinsText.text = "<sprite=0> " + shopData.cash;
                    yesSelected = true;
                    shopData.shopItems[currentIndex].isUnlocked=true;
                    UpgradeBtnStatus();
                    saveLoadData.SaveData();
                    SetCharacterInfo();  //update
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
                SetCharacterInfo(); //update speed
            }
        }
        private void UpgradeBtnMethcod()    
        {
            saveLoadData.SaveData();
            int nextLevelIndex = shopData.shopItems[currentIndex].unlockedLevel + 1;

            if (shopData.cash >= shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost)
            {
                shopData.cash -= shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost;
                totalCoinsText.text = "<sprite=8> " + shopData.cash;
                shopData.shopItems[currentIndex].unlockedLevel++;

                if (shopData.shopItems[currentIndex].unlockedLevel < shopData.shopItems[currentIndex].characterLevel.Length - 1)
                {
                    upgradeBtnText.text = "Upgrade \n <sprite=0>" + shopData.shopItems[currentIndex].characterLevel[nextLevelIndex + 1].unlockCost;
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
                unlockBtnText.text = "Cost \n <sprite=0>" + shopData.shopItems[currentIndex].unlockCost;
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
                    upgradeBtnText.text = "Upgrade \n <sprite=0>" + shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost;
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

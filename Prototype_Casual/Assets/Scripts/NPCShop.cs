using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem;

public class NPCShop : MonoBehaviour
{
    [SerializeField] GameObject circle;
    [SerializeField] private float rotationSpeed;
    public UI_Manager ui_manager;
    [SerializeField] Button NPCshopButton;
    [SerializeField] Button FirstNPCReady, SecondNPCReady;
    [SerializeField] GameObject NPC1, NPC2;
    [SerializeField] ShopUI shop;
    private void Start()
    {
        StartCoroutine(rotatorUpdate());

        if (PlayerPrefs.GetInt("NPC_1") == 1)
        {
            SpawnNPC1();
            FirstNPCReady.interactable = false;
        }
        if(PlayerPrefs.GetInt("NPC_2") == 1)
        {
            SpawnNPC2();
            SecondNPCReady.interactable = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zone"))
        {
            ui_manager.NPCShopButtonShow();
            //shopButton.gameObject.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zone"))
        {
            ui_manager.NPCShopButtonHide();
            ui_manager.HideNPC_HUD();
            //shopButton.gameObject.SetActive(false);
        }
    }
    IEnumerator rotatorUpdate()
    {
        while (true)
        {
            circle.transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime, Space.Self);
            yield return new WaitForSeconds(0.06f);
        }

    }
    public void SpawnNPC1()
    {
        NPC1.SetActive(true);
        FirstNPCReady.interactable = false;
    }
    public void BuyNPC1()
    {
        if (shop.shopData.cash >= 500)
        {
            NPC1.SetActive(true);
            PlayerPrefs.SetInt(("NPC_1"), 1);
            FirstNPCReady.interactable = false;
            shop.shopData.cash -= 500;
            shop.totalCoinsText.text = "<sprite=0> " + shop.shopData.cash;
            shop.saveLoadData.SaveData();
        }
        else
        {
            ui_manager.NoMoneyAnimation();
        }
    }
    public void SpawnNPC2()
    {
        NPC2.SetActive(true);
        SecondNPCReady.interactable = false;
    }
    public void BuyNPC2()
    {
        if (shop.shopData.cash >= 7000)
        {
            NPC2.SetActive(true);
            PlayerPrefs.SetInt(("NPC_2"), 1);
            SecondNPCReady.interactable = false;
            shop.shopData.cash -= 7000;
            shop.totalCoinsText.text = "<sprite=0> " + shop.shopData.cash;
            shop.saveLoadData.SaveData();
        }
        else
        {
            ui_manager.NoMoneyAnimation();
        }
    }
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.SetInt(("NPC_1"), 0);
        PlayerPrefs.SetInt("NPC_2", 0);
    }
}

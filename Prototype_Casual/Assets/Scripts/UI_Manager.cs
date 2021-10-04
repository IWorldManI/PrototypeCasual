using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem;


public class UI_Manager : MonoBehaviour
{
    public RectTransform main, shop, shopButton, npcShopB, npcShopPanel, NoMoneyMessage;
    public GameObject characterHolder, mainUI, shopUI, npcUI, shopButtonObject, npcShopButton, NoMoneyMessageObject;

    public GameObject coinText, boxText;

    public GameObject bussinessUI;
    public GameObject buyBussiness, collectBoxesInBussiness;

    public GameObject settingsUI, settingsButton;

    public Rotator rotator;

    private void Start()
    {
        buyBussiness.GetComponent<GameObject>();
        collectBoxesInBussiness.GetComponent<GameObject>();
        bussinessUI.GetComponent<GameObject>();
        characterHolder.GetComponent<GameObject>();
        mainUI.GetComponent<GameObject>();
        shopUI.GetComponent<GameObject>();
        npcUI.GetComponent<GameObject>();
        NoMoneyMessageObject.GetComponent<GameObject>();

                                                        //PlayerPrefs.SetInt("Business_0", 0);
        if (PlayerPrefs.GetInt("Business_0") == 1)
        {
            collectBoxesInBussiness.SetActive(true);
        }
        else
        {
            buyBussiness.SetActive(true);
        }
    }
    //Main hud settings
    public void Main_HUD()
    {
        LeanTween.moveX(shop, -2000f, .5f);
        LeanTween.moveY(main, 0f, .5f);
        LeanTween.moveY(characterHolder, 100f, 1f).setOnComplete(HideShopUI);
        mainUI.SetActive(true);
        
    }
    void HideShopUI()
    {
        characterHolder.SetActive(false);
        shopUI.SetActive(false);
    }
    //Shop HUD settings 
    public void Shop_HUD()
    {
        LeanTween.moveY(main, -2000f, .5f);
        LeanTween.moveX(shop, 0f, .5f);
        characterHolder.SetActive(true);
        rotator.startCor();
        LeanTween.moveY(characterHolder, 2f, .7f).setOnComplete(HideMainUI);
        shopUI.SetActive(true);

    }
    void HideMainUI()
    {
        mainUI.SetActive(false);
    }
    //NPC hud settings 
    public void NPC_HUD()
    {
        NPCShopButtonHide();
        LeanTween.moveY(npcShopPanel, 200f, .2f);
        npcUI.SetActive(true);
    }
    public void DisableNPC_HUD()
    {
        npcUI.SetActive(false);
    }
    public void HideNPC_HUD()
    {
        LeanTween.moveY(npcShopPanel, 1500f, .2f).setOnComplete(DisableNPC_HUD);
    }

    //Shop button hide/show
    public void ShopButtonShow()
    {
        LeanTween.moveY(shopButton, -800f, .3f);
        shopButtonObject.SetActive(true);
    }
    public void ShopButtonShowStay()
    {
        shopButtonObject.SetActive(true);
    }
    public void ShopButtonHide()
    {
        LeanTween.moveY(shopButton, -1500f, .3f).setOnComplete(HideShopButton);
    }
    void HideShopButton()
    {
        shopButtonObject.SetActive(false);
    }

    //NPCShop button hide/show
    public void NPCShopButtonShow()
    {
        LeanTween.moveY(npcShopB, -800f, .3f);
        npcShopButton.SetActive(true);
    }
    public void NPCShopButtonHide()
    {
        LeanTween.moveY(npcShopB, -1500f, .3f).setOnComplete(HideNPCShopButton);
    }
    void HideNPCShopButton()
    {
        npcShopButton.SetActive(false);
    }
    //Message if player dont have money to buy something
    public void NoMoneyAnimation()
    {
        NoMoneyMessageObject.SetActive(true);
        NoMoneyMessage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        LeanTween.scale(NoMoneyMessage, new Vector3(1f, 1f, 1f), 0.2f);
        LeanTween.scale(NoMoneyMessage, new Vector3(1f, 1f, 1f)*1.2f, 0.5f).setDelay(0.2f).setEasePunch();
        LeanTween.scale(NoMoneyMessage, new Vector3(0.1f, 0.1f, 0.1f), 0.1f).setDelay(1f).setOnComplete(HideMessage);
    }
    void HideMessage()
    {
        NoMoneyMessageObject.SetActive(false);
        NoMoneyMessage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
    public void AnimatedTextScaleCoin()
    {
        LeanTween.scale(coinText, new Vector3(1.3f, 1.3f, 1.3f), 0.8f).setEasePunch();  //mb need fix
    }
    public void AnimatedTextScaleBox()
    {
        LeanTween.scale(boxText, new Vector3(1.2f, 1.2f, 1.2f), 0.8f).setEasePunch();   //mb need fix
    }
    //BUSSINESS UI
    public void ShowBussinessWindow()
    {
        bussinessUI.SetActive(true);
        LeanTween.scale(bussinessUI, new Vector3(1.1f, 1.1f, 1.1f), 0.8f).setEasePunch();
    }
    public void CloseBussinessWindow()
    {
        bussinessUI.SetActive(false);
    }
    //SETTINGS UI
    public void ShowSettings()
    {
        settingsUI.SetActive(true);
        settingsButton.SetActive(false);
        LeanTween.scale(settingsUI, new Vector3(2.1f, 2.1f, 2.1f), 0.8f).setEasePunch();
    }
    public void CloseSettings()
    {
        settingsUI.SetActive(false);
        settingsButton.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    public RectTransform main, shop, shopButton;
    public GameObject characterHolder, mainUI, shopUI;
    

    public void Main_HUD()
    {
        LeanTween.moveX(shop, -2000f, .5f);
        LeanTween.moveY(main, 0f, .5f);
        LeanTween.moveY(characterHolder, 100f, 1f).setOnComplete(HideShopUI);
        mainUI.SetActive(true);
        
    }
    void HideShopUI()
    {
        shopUI.SetActive(false);
    }
    public void Shop_HUD()
    {
        LeanTween.moveY(main, -2000f, .5f);
        LeanTween.moveX(shop, 0f, .5f);
        LeanTween.moveY(characterHolder, 2f, .7f).setOnComplete(HideMainUI);
        shopUI.SetActive(true);

    }
    void HideMainUI()
    {
        mainUI.SetActive(false);
    }
    public void ShopButtonShow()
    {
        LeanTween.moveY(shopButton, -800f, .5f);
    }
    public void ShopButtonHide()
    {
        LeanTween.moveY(shopButton, -2000f, .5f);
    }
    
}

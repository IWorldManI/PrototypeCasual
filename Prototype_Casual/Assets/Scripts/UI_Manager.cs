using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    public RectTransform main, shop;
    public GameObject characterHolder;
    

    public void Main_HUD()
    {
        LeanTween.moveX(shop, -2000f, .5f);
        LeanTween.moveY(main, 0f, .5f);
        LeanTween.moveY(characterHolder, 100f, 1f);
        
    }

    public void Shop_HUD()
    {
        LeanTween.moveY(main, -2000f, .5f);
        LeanTween.moveX(shop, 0f, .5f);
        LeanTween.moveY(characterHolder, 2f, .7f);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShopSystem;
using TMPro;

public class BusinessBuilding : MonoBehaviour
{
    public Renderer circle;
    [SerializeField] float fillCircle = 360;
    [SerializeField] float fillSpeed=1f;        //0,1f faster, 1f optimized value
    [SerializeField] public int boxes;
    public PlayerController player;
    public UI_Manager uimanager;
    [SerializeField] ShopUI shop;
    public GameObject buyBussiness, collectBoxesInBussiness;
    public TextMeshProUGUI boxCount;

    private void Start()
    {
        //PlayerPrefs.SetInt("Business_0", 0);
        if (PlayerPrefs.GetInt("Business_0") == 1)
        {
            StartCoroutine(FillCircleFunction());

        }

    }
    private void OnTriggerEnter(Collider other)                                 //show bussiness ui
    {
        if (other.CompareTag("Zone"))
        {
            uimanager.ShowBussinessWindow();
            boxCount.text = "<sprite=1>" + boxes;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            uimanager.CloseBussinessWindow();
        }
    }

    private void OnTriggerStay(Collider other)                                  //update the number of collected boxes
    {
        if (other.CompareTag("Zone"))
        {
            boxCount.text = "<sprite=1>" + boxes;
        }
    }

    IEnumerator FillCircleFunction()
    {
        yield return new WaitForSeconds(fillSpeed);
        if (fillCircle != 0)
        {
            SetValue();
            StartCoroutine(FillCircleFunction());
        }
    }
    void SetValue()
    {
        fillCircle -= 1;
        circle.material.SetFloat("_Arc1", fillCircle);
        boxes += 1;
    }
    public void CollectBoxBussiness()                                           //this function is triggered by a button and transfers all earned boxes to the player
    {
        player.trash += boxes;
        player.trashTotal.text = "<sprite=1> " + player.trash;                  //update player box text
        boxes = 0;                                                              
        fillCircle = 360;                                                       //clear circle fill
        uimanager.AnimatedTextScaleBox();                                       //text scale animation
        boxCount.text = "<sprite=1>" + boxes;
        if (fillCircle == 0)                                                    
        {
            StartCoroutine(FillCircleFunction());                               
        }
    }
    public void BuyBussiness()                                                  //function for buying a business
    {
        if (shop.shopData.cash >= 5000)
        {
            shop.shopData.cash -= 5000;
            shop.totalCoinsText.text = "<sprite=0> " + shop.shopData.cash;
            shop.saveLoadData.SaveData();
            buyBussiness.SetActive(false);                                      //disable buy business button
            collectBoxesInBussiness.SetActive(true);                            //turn on the button for collecting earned boxes
            PlayerPrefs.SetInt("Business_0", 1);                                //we save the purchase
            StartCoroutine(FillCircleFunction());                               //this coroutine starts collecting boxes in business
        }
        else
        {
            uimanager.NoMoneyAnimation();
        }

    }
}

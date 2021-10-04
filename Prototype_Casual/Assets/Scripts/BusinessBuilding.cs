using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShopSystem;
using TMPro;

public class BusinessBuilding : MonoBehaviour
{
    public Renderer circle;
    [SerializeField] float fillCircle = 360;
    [SerializeField] float fillSpeed=1f; //0,1f faster, 1f optimized
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
    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerStay(Collider other)
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
    public void CollectBoxBussiness()
    {
        player.trash += boxes;
        player.trashTotal.text = "<sprite=1> " + player.trash;
        boxes = 0;
        fillCircle = 360;
        uimanager.AnimatedTextScaleBox();
        boxCount.text = "<sprite=1>" + boxes;
        if (fillCircle == 0)
        {
            StartCoroutine(FillCircleFunction());
        }
    }
    public void BuyBussiness()
    {
        if (shop.shopData.cash >= 5000)
        {
            PlayerPrefs.SetInt("Business_0", 1);
            shop.shopData.cash -= 5000;
            shop.totalCoinsText.text = "<sprite=0> " + shop.shopData.cash;
            shop.saveLoadData.SaveData();
            buyBussiness.SetActive(false);
            collectBoxesInBussiness.SetActive(true);
            StartCoroutine(FillCircleFunction());
        }
        else
        {
            uimanager.NoMoneyAnimation();
        }

    }
}

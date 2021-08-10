using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop_Point : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] GameObject circle;
    [SerializeField] GameObject shopObject;
    [SerializeField] Button shopButton;
    [SerializeField] Transform coinTrade;

    [SerializeField] float tweenTime;
    [SerializeField] float tweenScale;
    bool workCoroutine = true;

    public UI_Manager ui_manager;

    private void Start()
    {
        Vibration.Init();
        StartCoroutine(rotatorUpdate());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ui_manager.ShopButtonShow();
            //shopButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ui_manager.ShopButtonHide();
            //shopButton.gameObject.SetActive(false);
        }
    }

    IEnumerator rotatorUpdate()
    {
        while (workCoroutine)
        {
            circle.transform.Rotate(0,0, rotationSpeed * Time.fixedDeltaTime,  Space.Self);
            yield return new WaitForSeconds(0.06f);
        }

    }

    public void AnimationShop()
    {
        Instantiate(coinTrade, transform.position,transform.rotation * Quaternion.Euler(-90f,0f,0f));
        shopObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        LeanTween.scale(shopObject, new Vector3(0.8f, 0.8f, 0.8f) * tweenScale, tweenTime).setEasePunch(); //cute animation for sell boxes
        Vibration.VibratePeek(); //add vibro to sell boxes
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Point : MonoBehaviour
{

    public UI_Manager ui_manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ui_manager.Shop_HUD();
        }
    }
}

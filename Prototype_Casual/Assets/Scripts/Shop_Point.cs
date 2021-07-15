using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Point : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    public GameObject circle;

    public UI_Manager ui_manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ui_manager.Shop_HUD();
        }
    }
    private void FixedUpdate()
    {
        circle.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }
}

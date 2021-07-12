using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public Joystick joystick;
    public float rotationSpeed;
    public GameObject[] characterModels;

    public int dataIndex;

    public int trash;
    public Text trashTotal;

    private void Start()
    {
        trashTotal.text = "" + trash;
    }
    public void SetSkin()
    {
        if (dataIndex == 0)
        {
            characterModels[0].SetActive(true);
            characterModels[1].SetActive(false);
            characterModels[2].SetActive(false);
        }
        else if (dataIndex == 1) 
        {
            characterModels[0].SetActive(false);
            characterModels[1].SetActive(true);
            characterModels[2].SetActive(false);
        }
        else if (dataIndex == 2)
        {
            characterModels[0].SetActive(false);
            characterModels[1].SetActive(false);
            characterModels[2].SetActive(true);
        }

    }
    private void FixedUpdate()
    {
        float xMovement = joystick.Horizontal;
        float zMovement = joystick.Vertical;

        
        transform.position += new Vector3(xMovement, 0f, zMovement) * speed * Time.fixedDeltaTime;

        Vector3 movementDirection = new Vector3(xMovement, 0, zMovement);
        movementDirection.Normalize();

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}


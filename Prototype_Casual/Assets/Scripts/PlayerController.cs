using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem;
using System;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public Joystick joystick;
    public float rotationSpeed;
    public GameObject[] characterModels;


    public Camera Cam;
    
    public int dataIndex;

    public int trash;
    public Text trashTotal;

    private void Start()
    {
        trashTotal.text = "" + trash;
    }
    public void SetSkin()
    {
        for (int i=0; i < characterModels.Length; i++)  //disable all skins 
        {
            characterModels[i].SetActive(false);
        }
        characterModels[dataIndex].SetActive(true);     //enable selected skin

    }
    private void FixedUpdate()
    {
        float xMovement = joystick.Horizontal;
        float zMovement = joystick.Vertical;

        
        transform.position += new Vector3(xMovement, 0f, zMovement) * speed * Time.fixedDeltaTime;

        //rb.velocity += new Vector3(xMovement, 0f, zMovement) * speed / 10;

        Vector3 movementDirection = new Vector3(xMovement, 0, zMovement);
        movementDirection.Normalize();
        


        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);          
        }
        
    }
    
}


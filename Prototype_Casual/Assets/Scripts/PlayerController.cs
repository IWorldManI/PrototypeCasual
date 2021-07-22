using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem;
using System;


public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject characterHolder;
    [SerializeField] float tweenScale;
    public float speed;
    public Joystick joystick;
    public float rotationSpeed;
    public GameObject[] characterModels;
    public Rigidbody rb;
    public float maxSpeed;

    public Camera Cam;
    
    public int dataIndex;

    public int trash;
    public Text trashTotal;

    private void Start()
    {
        trashTotal.text = "" + trash;
        rb.GetComponent<Rigidbody>();
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
        /*
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        rb.velocity += new Vector3(xMovement, 0f, zMovement) * speed / 10;
        */

        Vector3 movementDirection = new Vector3(xMovement, 0, zMovement);
        movementDirection.Normalize();
        


        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);          
        }
        
    }
    public void PickUpAnimation()
    {
        characterHolder.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        LeanTween.scale(characterHolder, new Vector3(0.8f, 0.8f, 0.8f) * tweenScale, .8f).setEasePunch();
    }
}


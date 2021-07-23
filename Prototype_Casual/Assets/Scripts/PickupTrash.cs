﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace ShopSystem
{
    public class PickupTrash : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Collider sphereCollider;
        public PlayerController player;     //add amount here
        public float fieldOfPickup;
        [SerializeField] Slider slider;               //reference slider
        public float FillSpeed = 10f;       //speed how fast can you pick up an item 
        [SerializeField] private float targetProgress = 0;
        [SerializeField] float speedOfBoxFly;
        [SerializeField] GameObject boxPrefab;

        
        

        private void Start()
        {
            GameObject child = this.transform.GetChild(0).gameObject;       //do child circle
            UpdateCircleRadius();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("PickedUp") && slider.value < 1)
            {
                IncrementProgress(1f);

                FadeInCircle();

                slider.enabled = true;

                Debug.Log("Pick up");
            }
            else if (other.gameObject.CompareTag("PickedUp") && slider.value >= 1)
            {
                PickUp();

                Destroy(other.gameObject);

                FadeOutCircle();

                PickUpBoxAnim(other.transform.position, () => { player.PickUpAnimation(); });
                //player.PickUpAnimation();

                Debug.Log("Catch up");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PickedUp"))
            {
                slider.value = 0f;

                FadeOutCircle();

                slider.enabled = false;
            }

        }


        public void PickUp()
        {
            FadeOutCircle();

            //Collider[] objects = Physics.OverlapSphere(transform.position, fieldOfPickup); test

            player.trash += UnityEngine.Random.Range(1, 5);         //add amount of box/trash/something
            player.trashTotal.text = "" + player.trash; //update raised items

            slider.value = 0f;

        }

        void FadeInCircle()
        {
            GameObject child = this.transform.GetChild(0).gameObject;
            LeanTween.alpha(child, 1f, targetProgress);
        }
        void FadeOutCircle()
        {
            GameObject child = this.transform.GetChild(0).gameObject;
            LeanTween.alpha(child, 0.2f, targetProgress);
        }
        void IncrementProgress(float newProgress)
        {
            targetProgress = slider.value + newProgress;

            if (slider.value < targetProgress)
            {
                slider.value += FillSpeed * Time.fixedDeltaTime;
            }
        }

        //Animation for box cathcing

        void PickUpBoxAnim(Vector3 _initial, Action onComplete)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y + 1, target.position.z - 2);
            GameObject _box = Instantiate(boxPrefab,player.transform);
            Debug.Log(_box.transform);

            StartCoroutine(MoveBox(_box.transform, _initial, targetPos, onComplete));
        }

        IEnumerator MoveBox(Transform obj, Vector3 startPos, Vector3 endPos, Action onComplete)
        {
            float time = 0;
            while (time < 1)
            {
                time += speedOfBoxFly * Time.deltaTime;
                obj.position = Vector3.Lerp(startPos, endPos, time);
                yield return new WaitForEndOfFrame();
            }
            onComplete.Invoke();
            Destroy(obj.gameObject);
        }
        
        public void UpdateCircleRadius()
        {
            sphereCollider.transform.localScale = new Vector3(fieldOfPickup, fieldOfPickup, fieldOfPickup);
        }
        private void OnDrawGizmos()            //draw fizmos in editor
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fieldOfPickup);
        }

    }

}
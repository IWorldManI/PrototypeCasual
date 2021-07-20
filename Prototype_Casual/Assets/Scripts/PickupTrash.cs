using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ShopSystem
{
    public class PickupTrash : MonoBehaviour
    {
        [SerializeField]

        
        public PlayerController player;     //add amount here
        public float fieldOfPickup;         
        public Slider slider;               //reference slider
        public float FillSpeed = 10f;       //speed how fast can you pick up an item 
        private float targetProgress = 0;

        
        

        private void Start()
        {
            GameObject child = this.transform.GetChild(0).gameObject;       //do child for spawner
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
            GameObject child = this.transform.GetChild(0).gameObject;
            LeanTween.alpha(child, 0.2f, .5f);

            //Collider[] objects = Physics.OverlapSphere(transform.position, fieldOfPickup); test

            player.trash += Random.Range(1, 5);         //add amount of box/trash/something
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


        private void OnDrawGizmos()            //draw fizmos in editor
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fieldOfPickup);
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ShopSystem
{
    public class PickupTrash : MonoBehaviour
    {
        [SerializeField]

        
        public PlayerController player;
        public float fieldOfPickup;
        public Slider slider;
        public float FillSpeed = 10f;
        private float targetProgress = 0;

        
        

        private void Start()
        {
            GameObject child = this.transform.GetChild(0).gameObject;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("PickedUp") && slider.value < 1)
            {
                //StartCoroutine(Timer());
                IncrementProgress(1f);

                GameObject child = this.transform.GetChild(0).gameObject;
                LeanTween.alpha(child, 1f, targetProgress);

                slider.enabled = true;

                Debug.Log("Pick up");
            }
            else if (other.gameObject.CompareTag("PickedUp") && slider.value >= 1)
            {
                PickUp();

                Destroy(other.gameObject);

                GameObject child = this.transform.GetChild(0).gameObject;
                LeanTween.alpha(child, 0.2f, targetProgress);

                Debug.Log("Catch up");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PickedUp"))
            {
                slider.value = 0f;

                GameObject child = this.transform.GetChild(0).gameObject;
                LeanTween.alpha(child, 0.2f, targetProgress);
                                              
                slider.enabled = false;
            }


        }


        public void PickUp()
        {
            GameObject child = this.transform.GetChild(0).gameObject;
            LeanTween.alpha(child, 0.2f, .5f);

            Collider[] objects = Physics.OverlapSphere(transform.position, fieldOfPickup);

            player.trash += Random.Range(1, 5);
            player.trashTotal.text = "" + player.trash; 

            slider.value = 0f;

        }

        void IncrementProgress(float newProgress)
        {
            targetProgress = slider.value + newProgress;

            if (slider.value < targetProgress)
                slider.value += FillSpeed * Time.fixedDeltaTime;

        }


        IEnumerator Timer()
        {
            yield return new WaitForSeconds(0.01f);
            if (slider.value < targetProgress)
                slider.value += FillSpeed * Time.deltaTime;
            else
            {
                PickUp();
            }
            Debug.Log("Catch up");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fieldOfPickup);
        }

    }

}
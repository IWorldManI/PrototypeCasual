using System.Collections;
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
        public PlayerController player;                                     //add amount here
        public float fieldOfPickup;
        [SerializeField] Slider slider;                                     //reference slider
        public float FillSpeed = 10f;                                       //speed how fast can you pick up an item 
        [SerializeField] private float targetProgress = 0;
        [SerializeField] float speedOfBoxFly;
        [SerializeField] GameObject boxPrefab;
        public GameObject boxText;
        public GameObject uiFactorx2;
        [SerializeField] public bool x2factorEnabled=false;
        public CountDownTimer timer;

        public AudioSource soundFX;

        [SerializeField] List<GameObject> collidedObj = new List<GameObject>();

        private void Start()
        {
            Vibration.Init();                                               //initialization vibro package 
            GameObject child = this.transform.GetChild(0).gameObject;       //do child circle
            UpdateCircleRadius();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickedUp"))
            {
                collidedObj.Add(other.gameObject);
            }      
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("PickedUp") && slider.value < 1 && collidedObj.Count != 0) 
            {
                IncrementProgress(1f);
                FadeInCircle();
                slider.enabled = true;
                //Debug.Log("Pick up");
                
            }
            else if (other.gameObject.CompareTag("PickedUp") && slider.value >= 1 && collidedObj.Count != 0)
            {
                PickUp();
               
                //Debug.Log("Catch up" + collidedObj[collidedObj.Count - 1].name);
            }
            
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PickedUp"))
            {
                slider.value = 0f;
                FadeOutCircle();
                slider.enabled = false;

                for (int i = 0; i < collidedObj.Count; i++)
                {
                    collidedObj.RemoveAt(collidedObj.Count - 1);
                }
            }
        }


        public void PickUp()
        {
            FadeOutCircle();                                                                    //play fade out animation with LeanTween
            if (x2factorEnabled)
            {
                player.trash += collidedObj[collidedObj.Count - 1].GetComponent<ItemCost>().boxCost * 2;
            }
            else
            {
                player.trash += collidedObj[collidedObj.Count - 1].GetComponent<ItemCost>().boxCost;  //add collected box to player with index "0"
            }
            
                                                                                                //remove pickedUp item from list with index "0"
            player.trashTotal.text = "<sprite=1> " + player.trash;                              //update raised items
            slider.value = 0f;                                                                  //reset value if player goes away

            LeanTween.scale(boxText, new Vector3(1.2f, 1.2f, 1.2f), 0.8f).setEasePunch();       //mb need fix

            PickUpBoxAnim(collidedObj[collidedObj.Count - 1].transform.position, () => { 
                player.PickUpAnimation(); 
                if(PlayerPrefs.GetInt("VibroEnabled", 1)==1)
                    Vibration.VibratePop();
                soundFX.pitch= UnityEngine.Random.Range(0.8f, 1.2f);
                soundFX.Play();
            }); //after collecting an item, animation and vibration are played

            if (collidedObj[collidedObj.Count - 1].GetComponent<ItemCost>().boxCost == 10)  //timer x2 boxes
            {
                uiFactorx2.SetActive(true);
                x2factorEnabled = true;
                timer.secondsLeft = 30;
            }
            Destroy(collidedObj[collidedObj.Count - 1]);
            collidedObj.RemoveAt(collidedObj.Count - 1);
            
            //Collider[] objects = Physics.OverlapSphere(transform.position, fieldOfPickup); //test WIP
        }
        
        void FadeInCircle()                                         //fade in animation 
        {
            GameObject child = this.transform.GetChild(0).gameObject;
            LeanTween.alpha(child, 1f, targetProgress);
        }
        void FadeOutCircle()                                        //fade out animation
        {
            GameObject child = this.transform.GetChild(0).gameObject;
            LeanTween.alpha(child, 0.2f, targetProgress);
        }
        void IncrementProgress(float newProgress)                   //function to fill progressbar 
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
            //Debug.Log(_box.transform);

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
        
        public void UpdateCircleRadius()                            //if the player bought an upgrade, the radius of the circle is updated
        {
            sphereCollider.transform.localScale = new Vector3(fieldOfPickup, fieldOfPickup, fieldOfPickup);
        }
        private void OnDrawGizmos()                                 //draw gizmos in editor
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, fieldOfPickup);
        }

    }

}
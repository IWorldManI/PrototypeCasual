using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace ShopSystem
{
    public class CountDownTimer : MonoBehaviour
    {
        public GameObject holderRimer;
        public GameObject textDisplay;
        public int secondsLeft;
        public bool takingAway = false;
        public PickupTrash pt;

        private void Start()
        {
            textDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;  //initialize the text
        }
        private void FixedUpdate()
        {
            if (takingAway == false && secondsLeft > 0)                             //сhecking whether the countdown and the remaining time are running
            {
                pt.x2factorEnabled = true;
                StartCoroutine(Timer());
            }
            else if(secondsLeft<=0)
            {
                pt.x2factorEnabled = false;
                holderRimer.gameObject.SetActive(false);
            }
        }
        IEnumerator Timer()                                                        //timer coroutine
        {
            takingAway = true;
            yield return new WaitForSeconds(1);                                    
            secondsLeft -= 1;                                                      //subtracting 1 from timer
            if (secondsLeft < 10)                                                  //text fix condition
            {
                textDisplay.GetComponent<TextMeshProUGUI>().text = "00:0" + secondsLeft;

            }
            else
            {
                textDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
            }
            takingAway = false;
        }
    }

}


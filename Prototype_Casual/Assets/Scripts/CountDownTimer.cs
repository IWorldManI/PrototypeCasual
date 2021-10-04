﻿using System.Collections;
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
            textDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
        }
        private void FixedUpdate()
        {
            if (takingAway == false && secondsLeft > 0)
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
        IEnumerator Timer()
        {
            takingAway = true;
            yield return new WaitForSeconds(1);
            secondsLeft -= 1;
            if (secondsLeft < 10)
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

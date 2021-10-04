using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotSpeed;
        [SerializeField] private Vector3 rotDirection;
        bool workCoroutine=true;

        private void Start()
        {
            StartCoroutine(rotatorUpdate());
        }
       
        IEnumerator rotatorUpdate()
        {
            while (workCoroutine)
            {
                transform.Rotate(rotDirection.x, rotSpeed * Time.fixedDeltaTime, rotDirection.z, Space.World);
                yield return new WaitForSeconds(0.06f);
            }
            
        }
        public void startCor()
        {
            StartCoroutine(rotatorUpdate());
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotSpeed;

        void FixedUpdate()
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.Self);
        }
    }

}


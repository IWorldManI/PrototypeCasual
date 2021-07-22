using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotSpeed;
        [SerializeField] private Vector3 rotDirection;

        void FixedUpdate()
        {
            transform.Rotate(rotDirection.x, rotSpeed * Time.deltaTime, rotDirection.z, Space.Self);
        }
    }

}


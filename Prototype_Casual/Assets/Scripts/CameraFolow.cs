using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    public Transform target;            //camera target
    public float smoothSpeed = 0.125f;
    public Vector3 offset;              //offset x y z

    public bool isZoomed = true;        //using for CameraZoom

    private void FixedUpdate()
    {
        Vector3 desirePosition = target.position + offset;                                          //enable offset
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition,smoothSpeed);    //smooth camera moving enable
        transform.position = smoothedPosition;                                                      //enable camera folow
        
    }
    
}
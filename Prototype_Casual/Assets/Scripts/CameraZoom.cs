using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;      //camera reference
    public int zoom = 90;   //value field of view in zoom
    public int normal = 70; //normal value field of view 
    public int smooth = 1;  //variable is multiplied by time for smooth changing camera field of view

    public CameraFolow camFolow;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))            //if button pressed camera zoom out     
        {
            camFolow.isZoomed = !camFolow.isZoomed; 
        }
        else if (Input.GetMouseButtonUp(0))         //if button realised camera zoom in
        {
            camFolow.isZoomed = false;
        }
        if (camFolow.isZoomed)                        
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }
    void ZoomOut() //zoom out function
    {
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
    }
    void ZoomIn() //zoom in function
    {
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
    }
}

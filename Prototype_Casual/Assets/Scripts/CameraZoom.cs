using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    public int zoom = 65;
    public int normal = 60;
    public int smooth = 1;

    public CameraFolow camFolow;

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            camFolow.isZoomed = !camFolow.isZoomed;
        }
        else if (Input.GetMouseButtonUp(0))
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
    void ZoomOut()
    {
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
    }
    void ZoomIn()
    {
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
    }
}

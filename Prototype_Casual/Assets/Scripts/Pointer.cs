using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform target;
    public float distance;



    private void LateUpdate()
    {
        var dir = target.position - transform.position;

        

        if (dir.magnitude < distance)
        {
            SetChildrenActive(false);

        }
        else
        {
            SetChildrenActive(true);

            this.transform.LookAt(target);
        }
    }
    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}

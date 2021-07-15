using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public Transform target;
    public float distance=2;
    public Text distText;
    int showDistance;

    private void Start()
    {
        StartCoroutine(DistenceToShop());
    }

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

    IEnumerator DistenceToShop() //coroutine to optimization?
    {
        while (true)
        {
            yield return new WaitForSeconds(.3f);  //update distance to shop
            if (target)
            {
                float dist = Vector3.Distance(target.position, transform.position);
                showDistance = Mathf.RoundToInt(dist);           //float to int 
                distText.text = showDistance.ToString() + "m";  //display to screen mb need space before "m"
            }
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

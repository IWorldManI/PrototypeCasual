using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject[] coinPickup;
    [SerializeField] public float timeToRespawn; //mb buy upgrade?
    [SerializeField] private bool spawning=true;
    [SerializeField] private int randomIndex;

    void Start()
    {
        SpawnBox();
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !spawning) 
        {
            if (transform.childCount == 0) 
            {
                StartCoroutine(Timer());
                spawning = true;
            }
            
        }
    }
    void SpawnBox()
    {
        randomIndex = Random.Range(0,4);

        var myNewBox = Instantiate(coinPickup[randomIndex], transform.position, Quaternion.identity);
        myNewBox.transform.parent = gameObject.transform;
        spawning = false;
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToRespawn = Random.Range(10f,15f));
        SpawnBox();
    }
}

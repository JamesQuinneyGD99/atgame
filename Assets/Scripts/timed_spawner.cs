using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timed_spawner : MonoBehaviour
{
    // This creates a gameobject every X seconds

    public float spawnDelay; // Delay between spawning objects
    public GameObject spawnObject; // The object that is spawned
    float nextSpawn; // When the next object will spawn

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // We check if enough time has passed to spawn a new object
        if(Time.time >= nextSpawn){
            nextSpawn = Time.time + spawnDelay; // We set the spawn time for the next object

            GameObject spawnedObject = Instantiate(spawnObject,transform.position,Quaternion.identity); // We spawn the object
            spawnedObject.transform.SetParent(transform.parent); // We attach the object to the background
        }
    }
}

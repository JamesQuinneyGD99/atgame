using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_manager : MonoBehaviour
{
    public GameObject currentBackground; // This is the background currently being used

    // This function loads a new background
    public void LoadBackground(GameObject newBackground,Vector3 pos){
        // We check to see if there is currently a background loaded
        if(currentBackground!=null){
            Destroy(currentBackground); // We remove the old background
        }
        currentBackground = Instantiate(newBackground); // We create our new background
        currentBackground.transform.SetParent(transform); // We attach the background to the background manager
        Camera.main.transform.position = pos; // We move the camera to the new position in the scene
    }
}

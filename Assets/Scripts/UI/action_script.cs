using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class action_script : MonoBehaviour
{
    public Text actionText; // This is the text that tells players how to do things
    public float nextReset; // This is how long until the action text resets itself

    // This sets the action text
    public void SetAText(string txt){
        actionText.text = txt; // This sets the text on screen
        nextReset = Time.time + 0.1f; // This sets when the text will reset
    }

    // Update is called once per frame
    void Update()
    {
        // We check to see if it is time to reset the action text
        if(Time.time >= nextReset){
            actionText.text = ""; // We reset the text
        }
    }
}

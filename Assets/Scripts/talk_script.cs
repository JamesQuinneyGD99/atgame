using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talk_script : MonoBehaviour
{
    public GameObject characterBubble; // This is the speech bubble which appears above the characters head
    public Text characterTalk; // This is the text which appears in the characters speech
    float hideBubble; // This is to determine when the character's bubble should hide

    // This makes the character speak
    public void DoTalk(string text,float waitTime){
        characterBubble.SetActive(true); // We make the character's bubble visible
        hideBubble = Time.time + waitTime; // We tell the bubble when it has to hide again
        characterTalk.text = text; // We set the text inside of the bubble
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // We check to see if the bubble should be hidden
        if(hideBubble != 0 && hideBubble < Time.time){
            hideBubble = 0; // This makes sure we only hide the bubble once
            characterBubble.SetActive(false); // We hide the bubble
            characterTalk.text = ""; // We reset the character's text
        }
    }
}

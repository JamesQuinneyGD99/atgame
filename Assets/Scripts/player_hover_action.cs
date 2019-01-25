using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_hover_action : MonoBehaviour
{
    public action_script actionText; // This allows us to set the action text
    public string hoverText; // This is the text when the player hovers over this
    bool isOver; // This is whether the player is over the object

    // Start is called before the first frame update
    void Start()
    {
        actionText = GameObject.Find("action-panel").GetComponent<action_script>(); // We find the action script
    }

    // Update is called once per frame
    void Update()
    {  
        if(isOver){
            actionText.SetAText(hoverText);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        // We check the object touching this
        GameObject player = collider.gameObject;

        // We check if the object touching is the player
        if(player.tag == "player"){
            isOver = true; // We tell the script that the player is over the object
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        // We check the object no longer touching this
        GameObject player = collider.gameObject;

        // We check if the object was touching the player
        if(player.tag == "player"){
            isOver = false; // We tell the script that the player is no longer over the object
        }
    }
}

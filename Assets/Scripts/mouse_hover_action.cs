using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_hover_action : MonoBehaviour
{
    public action_script actionText; // This allows us to set the action text
    public string hoverText; // This is the text when the player hovers over this
    bool isOver; // This is whether the mouse is currently over the object

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

    void OnMouseOver(){
        isOver = true; // We tell the script that the mouse is now over the object
    }

    void OnMouseExit(){
        isOver = false; // We tell the script that the mouse is no longer over the object
    }
}

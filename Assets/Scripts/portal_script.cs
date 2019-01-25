using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_script : MonoBehaviour
{
    bool touchingPlayer; // This is whether the portal is touching the player
    public bool clickable; // This is whether this object is only clicked
    game_master gameMaster; // This is the game master script

    public GameObject loadBackground; // This is the background that is loaded when the player loads this portal
    GameObject backgroundManager; // This is the object which the background is attached to

    // This loads the background in the portal
    void DoLoadPortal(){
        // We check that the current background isn't currently doing an action
       if(gameMaster.doTimeAction<Time.time){
            backgroundManager.GetComponent<background_manager>().LoadBackground(loadBackground,new Vector3(0.0f,0.0f,-10.0f)); // We load the background attached to this script
            gameMaster.OnEnterArea(loadBackground.name); // We tell the game master which area we have loaded
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        backgroundManager = GameObject.Find("Background Manager"); // We find the background manager when the portal spawns
        gameMaster = GameObject.Find("Game Master").GetComponent<game_master>(); // We find the game master script
    }

    void OnTriggerEnter2D(Collider2D collision){
        // We check if the player is touching the portal
        if(collision.gameObject == GameObject.FindGameObjectWithTag("player")){
            touchingPlayer=true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        // We check if the player stops touching the portal
        if(collision.gameObject == GameObject.FindGameObjectWithTag("player")){
            touchingPlayer=false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        // This is for when the portal is solid
        DoLoadPortal(); // Load our new background
    }

    // We check every frame
    void Update(){
        // We check if the player is holding the space bar
        if(Input.GetKeyDown("space") && !clickable){
            if(touchingPlayer){ // This is whether the player is touching the portal
                DoLoadPortal(); // Load our new background
            }
        }
    }

    void OnMouseDown(){
        // We check if the current background has a player, if it doesn't then we allow the portal to be clicked
        if(GameObject.FindGameObjectWithTag("player") == null || clickable){
            DoLoadPortal(); // Load our new background
        }
    }
}

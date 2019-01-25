using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag_script : MonoBehaviour
{
    bool touchingPlayer; // This is whether the bag is touching the player

    // When something touches the bag
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "player"){
            touchingPlayer = true; // We tell the game the player is now touching the bag
        }
    }

    // When something stops touching the bag
    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "player"){
            touchingPlayer = false; // We tell the game the player is no longer touching the bag
        }
    }

    void Update(){
        if(Input.GetKeyDown("space") && touchingPlayer){
            GameObject.Find("Inventory").GetComponent<money_script>().AddAvocado(100);
            GameObject.Find("cartel-boss").GetComponent<talk_script>().DoTalk("First time's free, come back when you need more",5);
            GameObject.Find("Game Master").GetComponent<game_master>().didPlayerTakeCash = true; // The player did take the offer
            GameObject.Find("Game Master").GetComponent<game_master>().offeredPlayer = false; // The player no longer has an offer as he has accepted the deal
            Destroy(this.gameObject); // We remove the bag
        }
    }
}

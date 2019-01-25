using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_master : MonoBehaviour
{
    public bool chairGuyDead; // This is whether the chair guy has been killed yet
    public bool offeredPlayer; // Whether the boss made the offer
    public bool didPlayerTakeCash; // This is whether the player took the cartel boss's offer
    string timedAction; // This is a timed action, they are called below
    public float doTimeAction; // This is the time we should do the action
    public Sprite deadSprite; // This is the sprite for the chair guy when he's already dead
    public GameObject avocadoBag; // This is the bag filled with avocados

    // Update is called once per frame
    void Update()
    {
        if(timedAction!=""&&doTimeAction<=Time.time){
            switch(timedAction){
                case "Kill Chair Guy": // This is if we are killing the chair guy
                    chairGuyDead = true;
                    GameObject.Find("cartel-boss").GetComponent<cartel_boss>().DoFire(); // Make the cartel boss fire his gun
                    timedAction = "Player Take Offer 1"; // Set up the chain of conversation
                    doTimeAction = Time.time + 1;
                    break;
                case "Player Take Offer 1": // The player makes a decision whether to take the cartels offer
                    GameObject.Find("cartel-boss").GetComponent<talk_script>().DoTalk("We're gonna need someone new to sell our shipment",3);
                    timedAction = "Player Take Offer 2"; // Next conversation topic
                    doTimeAction = Time.time + 3;
                    break;
                case "Player Take Offer 2": // The player makes a decision whether to take the cartels offer
                    GameObject.Find("cartel-boss").GetComponent<talk_script>().DoTalk("If you want the job then take the bag, otherwise leave",4);
                    GameObject.Find("avocado-bag").SetActive(true); // make the bag visible
                    offeredPlayer=true;
                    timedAction = ""; // Reset the timed action
                    break;
                default: // Ignore
                    timedAction = ""; // We reset the timed action
                    break;
            }
        }
    }

    // This is called when the user enters a new area
    public void OnEnterArea(string area){
        switch(area){
            case "shop-exterior": // when the user goes to the outer area of the avocado shop

                break;
            case "shop-interior": // when the user goes to the inner area of the avocado shop
                
                break;
            case "buy-exterior": // when the user goes to the outer area of the supermarket
                
                break;
            case "buy-interior": // when the user goes to the inner area of the supermarket

                break;
            case "warehouse": // when the user goes to the warehouse
                if(chairGuyDead){
                    GameObject.FindGameObjectWithTag("chair-guy").GetComponent<Animator>().SetFloat("spd",10000); // we make sure he's already dead
                    GameObject.Find("cartel-boss").GetComponent<cartel_boss>().shouldSwitch = true; // Make the boss turn around
                }
                else{
                    timedAction = "Kill Chair Guy"; // We tell the script we are going to kill the chair guy
                    doTimeAction = Time.time + 2.0f; // We kill him 2 seconds after the warehouse loads
                }
                break;
            case "map": // when the user opens the map
                if(offeredPlayer){
                    if(didPlayerTakeCash){
                        
                    }
                }
                break;
            default: // Ignore
                break;
        }
    }
}

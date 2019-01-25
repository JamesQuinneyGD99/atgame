using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buy_script : MonoBehaviour
{
    public int price; // This is how much this item costs
    public bool isAvocado; // Whether this gives the player an avocado
    public bool isBread; // Whether this gives the player bread
    public money_script playerInventory; // This is the player's inventory
    public Text buyText; // This is the text attached to the item

    // Start is called before the first frame update
    void Start()
    {  
        playerInventory = GameObject.Find("Inventory").GetComponent<money_script>(); // This finds the player's inventory
        buyText.text = "$"+price; // This displays the price under the item
    }

    void OnMouseDown(){
        // We check if the player can afford the item
        if(playerInventory.playerMoney>=price){
            // We take money off the player
            playerInventory.AddMoney(-price);
            
            // We check if the player is buying an avocado
            if(isAvocado){
                // We give the player an avocado
                playerInventory.AddAvocado(1);
            }
            // We check to see if the player is trying to buy
            else if(isBread){
                // We give the player some bread
                playerInventory.AddBread(1);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_spawner : MonoBehaviour
{
    public Transform parentObject; // This is the object that the spawned item will be attached to

    public bool isAvocado; // Whether this spawns avocados
    public bool isBread; // Whether this spawns bread

    money_script playerInventory; // This is the player's inventory

    public GameObject spawn; // This is the object that is spawned

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Inventory").GetComponent<money_script>(); // We find the player's inventory
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This is when the player clicks the spawner
    void OnMouseDown(){
        if(isAvocado){
            // We check if the player has any avocados
            if(playerInventory.playerAvocado>0){
                playerInventory.AddAvocado(-1); // This takes an avocado off the player
                GameObject avocado = Instantiate(spawn,transform.position,Quaternion.identity); // We create an avocado
                avocado.GetComponent<pickup_script>().OnMouseDown(); // We tell the avocado we want to pick it up
                avocado.transform.SetParent(parentObject); // We attach the item to the parent
            }
        }
        else if(isBread){
            // We check if the player has any bread
            if(playerInventory.playerBread>0){
                playerInventory.AddBread(-1); // We take a piece of bread off the player
                GameObject bread = Instantiate(spawn,transform.position,Quaternion.identity); // We create some bread
                bread.GetComponent<pickup_script>().OnMouseDown(); // We tell the bread we want to pick it up
                bread.transform.SetParent(parentObject); // We attach the item to the parent
            }
        }
        else{
            GameObject item = Instantiate(spawn,transform.position,Quaternion.identity); // We create the item
            item.GetComponent<pickup_script>().OnMouseDown(); // We tell the item we want to pick it up
            item.transform.SetParent(parentObject); // We attach the item to the parent
        }
    }
}

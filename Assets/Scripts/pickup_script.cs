using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_script : MonoBehaviour
{
    public bool pickedUp; // This is whether the object is picked up
    public static bool hasPickedUp; // This is whether the player is currently holding something
    public static float nextPickup; // This is when the player can next pick something up
    public static float lastPickup; // This is when the player can next pick something up
    int savedLayer; // This is the layer the item uses

    // Update is called once per frame
    void Update()
    {
        // We make the object follow the mouse if it is held
        if(pickedUp){
            // We check to see if this object is a child of another object or the player clicks
            if(transform.parent != null && (transform.parent.gameObject == null || transform.parent.gameObject.layer != 2)){
                this.gameObject.layer = savedLayer; // We restore the layer
                pickedUp = false; // We drop it
                hasPickedUp = false; // We tell the game that the player can now pick things up again
                return; // We end the function before moving the object
            }
            // We check if the mouse is down
            else if(Input.GetMouseButtonDown(0) && lastPickup!=Time.time){
                OnMouseDown(); // We tell the script that the mouse is clicked
            }

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition+ new Vector3(0.0f,0.0f,10.0f)); // Make make the object follow the mouse
        }
    }

    public void OnMouseDown(){
        // We check to see if this parent is independent from other objects
        if(transform.parent == null || (transform.parent.gameObject != null && transform.parent.gameObject.layer == 2)){
            // This checks to see if the player is holding this item
            if(hasPickedUp && pickedUp){
                // We store everything that this object is touching
                Collider2D[] objectTouching = Physics2D.OverlapPointAll(transform.position);

                // We check through everything the object is overlapping
                for(int i = 0;i<objectTouching.Length;i+=1){
                    // We check if the object is over the bin
                    if(objectTouching[i].gameObject.tag == "bin"){
                        Destroy(this.gameObject); // Remove remove the object

                        hasPickedUp = false; // We tell the game that the player can now pick up other objects
                        nextPickup = Time.time + 0.1f; // We make it so the player has to wait to pick something else up
                        return; // This ensures that no further code runs
                    }
                    // We check to see if we are placing the object on a plate and the object we are placing is not a plate
                    else if(objectTouching[i].gameObject.tag == "plate" && this.gameObject.tag != "plate"){
                        this.gameObject.layer = 2; // We make it so this object is ignored by raycasts
                        // We attach the object to the plate
                        transform.SetParent(objectTouching[i].gameObject.transform);
                        // We match the object rotation with the plate rotation
                        transform.localRotation = Quaternion.Euler(60.0f,0.0f,0.0f);
                        // We tell the script which object is now on the plate
                        switch(this.gameObject.tag){
                            // We check if the user is placing bread on the plate
                            case "bread":
                                if(this.gameObject.GetComponent<bread_script>().spread == null){
                                    // We add a slice of bread to the plate
                                    objectTouching[i].gameObject.GetComponent<plate_script>().onPlate["bread"] += 1;
                                }
                                else{
                                    // We tell the plate how much spread is on it
                                    objectTouching[i].gameObject.GetComponent<plate_script>().onPlate["avocado on toast spread"] += Mathf.FloorToInt(GetComponent<bread_script>().spread.transform.localScale.x*100);
                                    // We tell the plate how toasted the bread is
                                    objectTouching[i].gameObject.GetComponent<plate_script>().onPlate["avocado on toast toasted"] += Mathf.FloorToInt(GetComponent<bread_script>().toastPercent/25);
                                }
                                break;
                            // This is if the user is not placing bread on the plate
                            default:
                                // We add the item we just placed to the plate if it can be served
                                if(objectTouching[i].gameObject.GetComponent<plate_script>().onPlate.ContainsKey(this.gameObject.tag)){
                                    objectTouching[i].gameObject.GetComponent<plate_script>().onPlate[this.gameObject.tag]+=1;
                                }
                                break;
                        }

                        hasPickedUp = false; // We tell the game that the player can now pick up other objects
                        nextPickup = Time.time + 0.1f; // We make it so the player has to wait to pick something else up
                        return; // This ensures that no further code runs
                    }
                    // We check if we are placing a plate on a character
                    else if(objectTouching[i].gameObject.tag == "character" && this.gameObject.tag == "plate"){
                        Debug.Log(123);
                        bool correctOrder = true; // We start by assuming the order is correct, then we look for errors

                        foreach (KeyValuePair<string,int> entry in objectTouching[i].gameObject.GetComponent<order_script>().order){
                            switch(entry.Key){
                                case "avocado on toast spread":
                                    // Spread can be up to 5% off the requested value, otherwise the order is incorrect
                                    if(Mathf.Abs(entry.Value-GetComponent<plate_script>().onPlate[entry.Key])>5){
                                        correctOrder = false;
                                    } 
                                    break;
                                default:
                                    // If the order is either missing something or has too much of something we say the order is incorrect
                                    if(entry.Value!=GetComponent<plate_script>().onPlate[entry.Key]){
                                        correctOrder = false;
                                    }
                                    break;
                            }
                        }

                        if(correctOrder){
                            int bonus = 0; // Player's will receive a bonus based on how well toasted the bread is
                            int toasted = GetComponent<plate_script>().onPlate["avocado on toast toasted"]; // This is how toasted the bread is
                            switch(toasted){
                                case 0: // Not Toasted
                                    bonus = -2; // Player loses money if they don't bother toasting
                                    break;
                                case 1: // Slightly toasted
                                    bonus = 1; // Small bonus for making the effort to toast
                                    break;
                                case 2: // Perfect
                                    bonus = 3; // Big bonus for perfectly toasted
                                    break;
                                case 3: // Overdone
                                    bonus = 1; // Small bonus for accidentally going over
                                    break;
                                case 4: // Charred
                                    bonus = -5; // Major loss for serving customers charcoal
                                    break;
                                default: // This shouldn't ever run
                                    bonus = 0;
                                    break;
                            }

                            int orderCost = bonus + Random.Range(10,15); // This is how much you are getting paid for the order
                            objectTouching[i].GetComponent<order_script>().MakeOrder(); // We tell the character to make a new order
                            objectTouching[i].GetComponent<talk_script>().DoTalk("Thank You! Here's: $"+orderCost,5); // We tell the player how much money they made
                            GameObject.Find("Inventory").GetComponent<money_script>().AddMoney(orderCost); // We give the player their money
                            Destroy(this.gameObject); // We remove the plate
                        }
                    }
                }

                pickedUp = false; // We drop the current item
                hasPickedUp = false; // We tell the game that the player can now pick up other objects
                nextPickup = Time.time + 0.1f; // We make it so the player has to wait to pick something else up
                this.gameObject.layer = savedLayer; // We restore the layer
            }

            // We check to see if the player isn't holding this item and if enough time has passed since they last dropped something
            if(!hasPickedUp && Time.time > nextPickup){
                hasPickedUp = true; // We tell the game that the player is now holding something
                pickedUp = true; // We tell this object that it is now being held
                savedLayer = this.gameObject.layer; // We save the layer of the object
                this.gameObject.layer = 2; // We make it so the object ignores raycasts
                lastPickup = Time.time;
            }
        }
        else{
            GameObject parentObject = transform.parent.gameObject; // We find the parent's gameobject
            // We check if the parent can be picked up
            if(parentObject.GetComponent<pickup_script>()!=null){
                parentObject.GetComponent<pickup_script>().OnMouseDown(); // We tell the parent that it was clicked instead
            }
        }
    }

    // This is when the object is removed from the scene
    void OnDestroy(){
        // We check to see if the player was holding the object
        if(pickedUp){
            hasPickedUp = false; // We tell the game that the player can now pick things up again
        }
    }
}

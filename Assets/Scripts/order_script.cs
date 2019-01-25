using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class order_script : MonoBehaviour
{
    public Dictionary<string,int> order; // This is the order for the character
    public Dictionary<string,string> orderAlias; // Alternate names for order items
    string orderText; // This is character's order that appears above their head

    // Start is called before the first frame update
    void Start()
    {
        // This just makes the names of the items look nices by adding capitalisation
        orderAlias = new Dictionary<string,string>();

        orderAlias["knife"] = "Knife";
        orderAlias["avocado"] = "Avocado";
        orderAlias["bread"] = "Bread";
        orderAlias["avocado on toast spread"] = "Avocado on Toast";

        MakeOrder(); // We get the character to make an order
    }

    // This will set the order for the character
    public void MakeOrder(){
        // We clear any previous orders
        order = new Dictionary<string,int>();

        order["knife"] = Random.Range(0,2);
        order["avocado"] = Random.Range(0,3);
        order["bread"] = Random.Range(0,3);
        order["avocado on toast spread"] = Random.Range(20,101);

        orderText = ""; // This is the text that will appear in the character's speech bubble

        // We check through everything the customer wants
        foreach (KeyValuePair<string,int> entry in order){
            // We check to see if the customer wants this item
            if(entry.Value!=0){
                // We update the order text and start a new line, avocado on toast is a percentage value
                if(entry.Key == "avocado on toast spread"){
                    orderText += orderAlias[entry.Key]+": "+entry.Value+"%\n";
                }
                else{
                    orderText += orderAlias[entry.Key]+": "+entry.Value+"\n";
                }
            }
        }
    }

    // We make the character tell us their order when we click them
    void OnMouseDown(){
        // We check if the character has an order
        if(orderText!=""){
            // We enable the speech bubble with the character's order
            GetComponent<talk_script>().DoTalk(orderText,5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

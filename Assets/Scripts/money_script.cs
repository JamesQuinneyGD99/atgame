using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money_script : MonoBehaviour
{
    public int playerMoney,playerAvocado,playerBread; // This is how much money, avocado and bread the player has
    public Text moneyText,avocadoText,breadText; // This is the text on the top of the screen for the player's money,avocados and bread
    public Text moneyTextBackground,avocadoTextBackground,breadTextBackground; // This is the background text which also needs to be updated
    float lastUpdatedMoney = 0; // This is the unity time that the money was last updated
    float lastUpdatedAvocado = 0; // This is the unity time that the avocado count was last updated
    float lastUpdatedBread = 0; // This is the unity time that the bread count was last updated

    // This will both set the player's money and update the money text
    public void AddMoney(int amount){
        playerMoney += amount; // Increase the player's money by the desired amount
        moneyText.text = "$"+playerMoney+".00"; // Update the player's money on screen
        moneyTextBackground.text = moneyText.text; // Update the background aswell
        lastUpdatedMoney = Time.time; // We tell the script when the money last changed
    }

    // This will set the player's avocado count and update the avocado count text
    public void AddAvocado(int amount){
        playerAvocado += amount; // Increase the player's avocado count by the desired amount
        avocadoText.text = "Avocado: "+playerAvocado; // Update the player's avocado count on screen
        avocadoTextBackground.text = avocadoText.text; // Update the background aswell
        lastUpdatedAvocado = Time.time; // We tell the script when the avocado count last changed
    }

    // This will set the player's avocado count and update the bread count text
    public void AddBread(int amount){
        playerBread += amount; // Increase the player's bread count by the desired amount
        breadText.text = "Bread: "+playerBread; // Update the player's bread count on screen
        breadTextBackground.text = breadText.text; // Update the background aswell
        lastUpdatedBread = Time.time; // We tell the script when the avocado count last changed
    }

    // Start is called before the first frame update
    void Start()
    {
        AddMoney(50); // Players start with $50
        AddAvocado(3); // Players start with 3 avocados
        AddBread(3); // Players start with 3 pieces of bread
    }

    // Update is called once per frame
    void Update()
    {
        // We check to see if the player's money was updated in the last second
        if(Time.time-lastUpdatedMoney<=1){
            float val = Time.time-lastUpdatedMoney; // This is a decimal value which increases until it reachest 1
            moneyText.color = new Color(val,1.0f,val,1.0f); // We slowly make the text white again
        }

        // We check to see if the player's avocado count was updated in the last second
        if(Time.time-lastUpdatedAvocado<=1){
            float val = Time.time-lastUpdatedAvocado; // This is a decimal value which increases until it reachest 1
            avocadoText.color = new Color(val,val,val,1.0f); // We slowly make the text white again
        }

        // We check to see if the player's bread count was updated in the last second
        if(Time.time-lastUpdatedBread<=1){
            float val = Time.time-lastUpdatedBread; // This is a decimal value which increases until it reachest 1
            breadText.color = new Color(1.0f,1.0f,val,1.0f); // We slowly make the text white again
        }
    }
}

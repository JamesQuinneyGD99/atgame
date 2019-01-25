using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bread_script : MonoBehaviour
{
    public float toastPercent; // This is how toasted the bread is
    public GameObject spread; // This is the spread that is on the toast
    public Sprite spreadSprite; // This is the sprite the spread uses
    public Text toastText; // This tells the player how much they have toasted the bread
    public Text spreadText; // This tells the player how much they have spread the avocado

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This checks if the bread is on a plate
        if(transform.parent == null || transform.parent.gameObject.tag != "plate"){
            Collider2D[] overlaps = Physics2D.OverlapPointAll(transform.position); // This is all gameobjects that are overlapping the bread
            bool canCook = true; // We set this to true if the bread is overlapping other bread
            GameObject grill = this.gameObject; // This is the grill, we set it to the bread to test later on

            // We loop through everything that is overlapping the bread, including the bread itself
            for(int i = 0;i<overlaps.Length;i+=1){
                // We check to see if the bread is overlapping other bread
                if(overlaps[i].gameObject.tag == "bread" && overlaps[i].gameObject != this.gameObject){
                    canCook = false; // We tell the bread it can't cook
                }
                // We check to see if the bread is touching a grill
                else if(overlaps[i].gameObject.tag == "grill"){
                    grill = overlaps[i].gameObject; // We tell the game what grill we are using
                }
                // We check to see if the bread is touching an avocado
                else if(spread == null && overlaps[i].gameObject.tag == "cut avocado" && overlaps[i].gameObject.transform.parent.gameObject.tag != "bread"){
                    spread = overlaps[i].gameObject; // We set the spread to the overlapping object
                    spread.transform.position = transform.position; // We put the spread on the bread
                    spread.transform.SetParent(transform); // We stick the spread to the bread
                    spread.GetComponent<SpriteRenderer>().sprite = spreadSprite; // We change the avocado to spread
                    spread.transform.localScale = new Vector2(0.2f,0.2f); // We make the spread only cover the middle of the bread

                    spreadText.text = Mathf.Floor(spread.transform.localScale.x*100.0f)+"% Spread"; // This updates how much avocado the player has spread
                }
            }

            if(canCook && grill != this.gameObject){ // We check to see if the bread can cook and if it is on a grill
                if(grill.GetComponent<grill_script>().isOn){ // We check to see if the grill is on
                    toastPercent = Mathf.Clamp(toastPercent+Time.deltaTime*10.0f,0.0f,100.0f); // We increase the amount the bread is toasted
                    int toastCase = Mathf.FloorToInt(toastPercent/25); // This gives our toast percent as a number from 0-4
                    string toastedText; // This is the text that shows how toasted the bread is
                    // This works out the text depending on how toasted the bread is
                    // A higher number after "case" means the bread is more toasted
                    switch(toastCase){
                        case 0:
                            toastedText = "Not Toasted";
                            break;
                        case 1:
                            toastedText = "Under Toasted";
                            break;
                        case 2:
                            toastedText = "Perfectly Toasted";
                            break;
                        case 3:
                            toastedText = "Over Toasted";
                            break;
                        case 4:
                            toastedText = "Charred";
                            break;
                        default: // Default should never run if the code is successful
                            toastedText = "Error!";
                            break;
                    }
                    toastText.text = toastedText; // This updates how much the bread has been toasted
                    float lerpMult = toastPercent/120.0f; // This is the multiplier and is applied differently for each color
                    GetComponent<SpriteRenderer>().color = new Color(1.0f-lerpMult*0.6f,1.0f-lerpMult*0.8f,1.0f-lerpMult); // We change the color of the toast
                }
            }
        }
    }
}

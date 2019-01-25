using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grill_script : MonoBehaviour
{
    public SpriteRenderer grills; // These are the grills attached to the grill
    public float heatPercent; // This is how hot the grill is
    public bool isOn; // This is whether the grill is on or not
    public Image grillImage; // This is the on button for the grill

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn){
            heatPercent = Mathf.Clamp(heatPercent+Time.deltaTime*12.0f,0.0f,100.0f); // We increase the heat if the grill is on
        }
        else{
            heatPercent = Mathf.Clamp(heatPercent-Time.deltaTime*12.0f,0.0f,100.0f); // We decrease the heat if the grill is off
        }

        grills.color = new Color(heatPercent/80.0f,0.0f,0.0f); // This increases the "redness" of the grill depending on the heat
    }

    public void grillButtonClick(){
        // If the grill is on, we turn it off, otherwise we turn it on
        isOn = !isOn;

        // If the grill is now on, we set the button color to green, otherwise we set it to red
        if(isOn){
            grillImage.color = new Color(0.0f,0.5f,0.0f,1.0f);
        }
        else{
            grillImage.color = new Color(0.5f,0.0f,0.0f,1.0f);
        }
    }
}

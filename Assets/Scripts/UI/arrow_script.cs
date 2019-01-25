using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_script : MonoBehaviour
{
    public GameObject swapObject; // This is the object where the Y-axis is swapped
    public float cameraMove; // This is where camera moves to when you click the arrow
    
    // Make the arrow transparent when our mouse hovers over it
    void OnMouseOver(){
        GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,0.5f);
    }

    // Remove the transparency when our mouse leaves the arrow
    void OnMouseExit(){
        GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,1.0f);
    }
    
    // This tells the screen to move
    void DoMove(){
        // We swap to the other view when the arrow is clicked
        Camera.main.transform.position = new Vector3(0.0f,cameraMove,-10.0f);
    }

    // This is when the user clicks the arrow
    void OnMouseDown(){
        DoMove(); // We tell the screen to move
    }
}

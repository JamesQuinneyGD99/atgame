using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartel_boss : MonoBehaviour
{
    public Sprite frame1; // This is the first frame of the boss's animation
    public Sprite frame2; // This is the second frame of the boss's animation
    public bool shouldSwitch; // This is whether we should switch back to the first frame
    float switchTime; // This is the time we should switch back to the first frame

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // We check if the character needs to switch back to the first frame
        if(shouldSwitch){
            // We check if the time to switch has come
            if(Time.time>switchTime){
                GetComponent<SpriteRenderer>().sprite = frame1; // We switch back to the first frame
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x),transform.localScale.y);
                shouldSwitch = false; // Stops it from switching every frame
            }
        }
    }

    public void DoFire(){
        shouldSwitch = true; // We tell the game to prepare to switch to the first frame
        switchTime = Time.time + 0.1f; // We switch back to the first animation in 0.5 seconds
        GetComponent<SpriteRenderer>().sprite = frame2; // Switch to the second frame
        GameObject.FindGameObjectWithTag("chair-guy").GetComponent<Animator>().SetFloat("spd",1.25f); // We "kill" the chair guy
    }
}

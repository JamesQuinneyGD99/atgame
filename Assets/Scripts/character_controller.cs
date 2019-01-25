using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character_controller : MonoBehaviour
{
    public Transform characterBase; // This is the base model for the character

    // These are the joints attached to the character
    public GameObject leftLeg,rightLeg,rightArm,LeftArm,LeftArmJoint,LeftLegJoint,RightLegJoint,RightArmJoint;
    // This is for controller the characters face
    public GameObject head,eyes,mouth,hair;

    // These are the alternate sprites used so the character can turn
    public Sprite eyesSide,eyesFront,hairSide,hairFront,mouthSide,mouthFront;

    public bool isMoving; // This is whether the movement animation should play

    public Rigidbody2D rb; // This is the rigidbody attached to the controller
    public bool playerDriven; // Whether this character is controlled by the player

    public void MoveForward(float mult){
        rb.velocity = new Vector2(mult,rb.velocity.y); // This will push the character to the right
        characterBase.localScale = new Vector2(Mathf.Abs(characterBase.localScale.x),Mathf.Abs(characterBase.localScale.y)); // This makes the character face forward
        isMoving=true; // We tell the script that the character just moved
    }

    public void MoveBackward(float mult){
        rb.velocity = new Vector2(mult,rb.velocity.y); // This will push the character to the left
        characterBase.localScale = new Vector2(-Mathf.Abs(characterBase.localScale.x),Mathf.Abs(characterBase.localScale.y)); // This makes the character face backward
        isMoving=true; // We tell the script that the character just moved
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDriven){
            isMoving = false; // We reset it every frame to see if we should play the animation
            float moveX = Input.GetAxis("Horizontal")*(Time.deltaTime*100.0f); // This is how fast the player is moving

            // We check if the player is trying to move forward
            if(moveX>0){
                MoveForward(moveX); // We move the player forward
            }
            // We check if the player is trying to move backward
            else if(moveX<0){
                MoveBackward(moveX); // We move the player backward
            }
        }

        // This checks if the character is no longer moving
        if(!isMoving){
            // We check to see if the character was just moving
            if(LeftArmJoint.GetComponent<joint_script>().isActive){
                // We reset all of the character's joints
                LeftArmJoint.GetComponent<joint_script>().curRot = 0;
                RightArmJoint.GetComponent<joint_script>().curRot = 0;
                RightLegJoint.GetComponent<joint_script>().curRot = 0;
                LeftLegJoint.GetComponent<joint_script>().curRot = 0;

                // This makes the character face forward when not moving
                eyes.GetComponent<SpriteRenderer>().sprite = eyesFront;
                mouth.GetComponent<SpriteRenderer>().sprite = mouthFront;
                hair.GetComponent<SpriteRenderer>().sprite = hairFront;
            }
        }
        else{
            // We check to see if the character was just standing still
            if(!LeftArmJoint.GetComponent<joint_script>().isActive){
                // This makes the character face to the side when moving
                eyes.GetComponent<SpriteRenderer>().sprite = eyesSide;
                mouth.GetComponent<SpriteRenderer>().sprite = mouthSide;
                hair.GetComponent<SpriteRenderer>().sprite = hairSide;
            }
        }
        // We tell the character's joints whether to move or not
        LeftArmJoint.GetComponent<joint_script>().isActive = isMoving;
        RightArmJoint.GetComponent<joint_script>().isActive = isMoving;
        RightLegJoint.GetComponent<joint_script>().isActive = isMoving;
        LeftLegJoint.GetComponent<joint_script>().isActive = isMoving;
    }
}

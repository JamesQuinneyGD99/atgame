using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joint_script : MonoBehaviour
{
    public float multiplier; // This affects the speed and direction of the joint's rotation
    public bool isActive; // This is whether the joint should currently be rotating
    public float angle; // This is the angle of rotation for the joint
    public float curRot; // This is the current rotation of the joint
    // Start is called before the first frame update
    void Start()
    {
        curRot = 1*multiplier; // Reset the rotation
    }

    // Update is called once per frame
    void Update()
    {
        // We check to see if the joint is currently being used by the character
        if(isActive){
            curRot+=multiplier*Time.deltaTime; // We increase the amount the joint is rotated
            transform.localRotation = Quaternion.Euler(0,0,Mathf.Sin(curRot)*angle); // We rotate the joint
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automove_script : MonoBehaviour
{
    // This auto moves an object either left or right constantly, used for cars, customers etc

    Rigidbody2D rb; // This is the rigidbody attached to the object 
    public float dirMult; // Multiplies force added to object each frame, negative to move left

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Find the object's rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(dirMult*150.0f*Time.deltaTime,0.0f); // Apply constant force
    }
}

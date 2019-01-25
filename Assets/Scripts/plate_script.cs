using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate_script : MonoBehaviour
{
    public Dictionary<string,int> onPlate; // This is everything on the plate

    void Start(){
        // We create a dictionary holding everything that can be placed on a plate
        onPlate = new Dictionary<string,int>();
        onPlate["knife"] = 0;
        onPlate["avocado"] = 0;
        onPlate["bread"] = 0;
        onPlate["avocado on toast spread"] = 0;
        onPlate["avocado on toast toasted"] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

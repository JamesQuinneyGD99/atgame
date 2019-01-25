using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class knife_script : MonoBehaviour
{
    public GameObject leftAvocado; // This is the left side of a cut avocado
    public GameObject rightAvocado; // This is the right side of a cut avocado
    Vector3 lastCutPos; // This is whether the knife was when it last cut something

    // Start is called before the first frame update
    void Start()
    {
        lastCutPos = transform.position; // This makes sure that there is always a position to check
    }

    // Update is called once per frame
    void Update()
    {
        // We check to make sure the knife has moved since it was last used
        if(transform.position != lastCutPos && GetComponent<pickup_script>().pickedUp){
            Collider2D[] overlaps = Physics2D.OverlapPointAll(transform.position); // Store everything the knife blade is overlapping

            // We loop through every avocado
            for(int i = 0;i<overlaps.Length;i+=1){
                // We check to see if there are any avocados
                if(overlaps[i].gameObject.tag == "avocado"){
                    // We check to make sure the avocado is not on a plate
                    if(overlaps[i].gameObject.transform.parent == null || overlaps[i].gameObject.transform.parent.gameObject.tag != "plate"){
                        overlaps[i].gameObject.GetComponent<avocado_script>().cut += Time.deltaTime; // We increase the amount the avocado is cut

                        // We check if the avocado is fully cut
                        if(overlaps[i].gameObject.GetComponent<avocado_script>().cut>1.0f){
                            Destroy(overlaps[i].gameObject); // We remove the avocado

                            GameObject left = Instantiate(leftAvocado,transform.position - new Vector3(0.5f,0.0f,0.0f),Quaternion.identity); // We create the left side of an avocado
                            GameObject right = Instantiate(rightAvocado,transform.position + new Vector3(0.5f,0.0f,0.0f),Quaternion.identity); // We create the right side of an avocado
                        
                            left.transform.SetParent(transform.parent); // We attach the left side avocado to the counter
                            right.transform.SetParent(transform.parent); // We attach the right side avocado to the counter
                        }
                        lastCutPos = transform.position; // We update where the knife last cut
                    }
                }
                // We check to see if the knife is overlapping bread
                else if(overlaps[i].gameObject.tag == "bread"){
                    // We store the bread's script for access in a second
                    bread_script breadScript = overlaps[i].gameObject.GetComponent<bread_script>();
                    // We check to see if there is any spread on the bread
                    if(breadScript.spread!=null){
                        Vector2 currentScale = breadScript.spread.transform.localScale; // This is the current size of the spread
                        float newScale = Mathf.Clamp(currentScale.x+Time.deltaTime/10.0f,0.0f,1.0f); // We work out the new size of the spread
                        breadScript.spread.transform.localScale = new Vector2(newScale,newScale); // We set the size of the spread
                        breadScript.spreadText.text = Mathf.Floor(breadScript.spread.transform.localScale.x*100.0f)+"% Spread";
                        lastCutPos = transform.position; // We update where the knife was last used
                    }
                }
            }  
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_spawner : MonoBehaviour
{
    public List<GameObject> characters; // These are the character prefabs that spawn
    public List<Vector2> positions; // These are where each character spawns
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<characters.Count;i+=1){
            GameObject curChar = Instantiate(characters[i],positions[i],Quaternion.identity);
            curChar.transform.SetParent(transform);
        }
    }
}

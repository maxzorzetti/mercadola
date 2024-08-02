using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{

    public Sprite[] sprites; 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Length)];        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

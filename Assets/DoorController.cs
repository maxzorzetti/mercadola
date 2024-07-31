using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("open");
        if ( other.tag == "Player" )
        {
            gameObject.GetComponent<Animator>().SetBool("OpenDoor", true);
        }
    }

        void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("close");
        if ( other.tag == "Player" )
        {
            gameObject.GetComponent<Animator>().SetBool("OpenDoor", false);
        }
    }
}

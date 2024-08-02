using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    SpriteRenderer playerSpriteRenderer;
    void Start()
    {
        playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerSpriteRenderer.flipX == true)
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }else  
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
            
    }
}

using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class WeatherController : MonoBehaviour
{

    private bool isRaining = false;
    bool didToggleRain = false;
    public ParticleSystem rainParticle;
    public GameObject rainOverlay;

    public float speed;
    Color starColor, endColor;



    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleRain()
    { 
        didToggleRain = true;
        if(isRaining)
        {
            rainParticle.Stop();
            starColor = rainOverlay.GetComponent<Image>().color;
            endColor = starColor;
            endColor.a = 0f;
            Debug.Log("oi 1");
            StartCoroutine(ChangeOverlayColor());
            //rainOverly.GetComponent<Image>().color = starColor;
            //rainOverly.SetActive(false);

            isRaining = !isRaining;
        }
        else
        {
            rainParticle.Play();
            starColor = rainOverlay.GetComponent<Image>().color;
            endColor = starColor;
            endColor.a = 1f;
            Debug.Log("oi 2");
            StartCoroutine(ChangeOverlayColor());
            //rainOverly.GetComponent<Image>().color = endColor;
            //rainOverly.SetActive(true);

            isRaining = !isRaining;
        }  
    }

    private IEnumerator ChangeOverlayColor()
    {
    Debug.Log("oi 3");
    float tick = 0f;
        while (rainOverlay.GetComponent<Image>().color != endColor && didToggleRain)
    {
        tick += Time.deltaTime * speed;
        rainOverlay.GetComponent<Image>().color = Color.Lerp(starColor, endColor, tick);
        yield return null;
    }
    didToggleRain = false;
    }
}

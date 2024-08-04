using UnityEngine;

public class DoorController : MonoBehaviour
{
    AudioSource SFXPlayer;
    public AudioClip[] SFXs;
    
    static readonly int OpenDoor = Animator.StringToHash("OpenDoor");

    void Awake()
    {
        SFXPlayer  = GetComponent<AudioSource>();
    }
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
        if (!other.CompareTag("Player")) return;
        
        SFXPlayer.clip = SFXs[0];
        gameObject.GetComponent<Animator>().SetBool(OpenDoor, true);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        SFXPlayer.clip = SFXs[1];
        gameObject.GetComponent<Animator>().SetBool(OpenDoor, false);
    }

    void PlayDoorSound()
    {
        SFXPlayer.Play();
    }
}

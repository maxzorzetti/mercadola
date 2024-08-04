using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Drifter : MonoBehaviour
{
    public AudioClip interferenceSound;
        
    public float acceleration = 1;
    public float maxSpeed = 0.25f;
    public float relentlessness = 0.05f;
    public float killRange = 0.45f;

    public float idleAnimationDistance = 13f;
    public float awakeAnimationDistance = 5f;
    public float killAnimationDistance = 3f;

    public int communicationFrequency = 10;
    int communicationProgress;
    
    Sprite[] sprites;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Transform player;
    Vector3 velocity;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Foreground";
        spriteRenderer.sortingOrder = 1000;
        sprites = Resources.LoadAll<Sprite>("Debug/Debug-sheet");
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        player = FindObjectOfType<Dola>().transform;
        
        HijackAudio();
    }

    void Update()
    {
        Move();
        var distance = DistanceToPlayer();

        maxSpeed += relentlessness * Time.deltaTime;
        acceleration += relentlessness * Time.deltaTime;
        
        Visual(distance);
        Communicate(distance);
        Kill(distance);
    }

    float DistanceToPlayer()
    {
        return Mathf.Abs(Vector3.Distance(player.position, transform.position));
    }

    void Move()
    {
        var position = transform.position;
        var direction = (player.position - position).normalized;

        velocity += (acceleration * direction) * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    void Visual(float distance)
    {
        if (distance > idleAnimationDistance)
        {
            spriteRenderer.sprite = sprites[0];
        } 
        else if (distance > awakeAnimationDistance)
        {
            var interpolation = (distance - awakeAnimationDistance) / (idleAnimationDistance - awakeAnimationDistance);
            var spriteToUse = (int)Mathf.Lerp(11, 1, interpolation);
            spriteRenderer.sprite = sprites[spriteToUse];    
        }
        else if (distance < killAnimationDistance)
        {
            var interpolation = (distance - killRange) / (killAnimationDistance - killRange);
            var spriteToUse = (int)Mathf.Lerp(14, 11, interpolation);
            spriteRenderer.sprite = sprites[spriteToUse];
            spriteRenderer.color = distance <= killRange * 1.05 ? Color.red : Color.white;
        }
    }

    void Communicate(float distance)
    {
        if (distance < killAnimationDistance && communicationProgress % communicationFrequency == 0)
        {
            var message = string.Concat(Enumerable.Repeat("💧︎💣︎☜︎☹︎☹︎💧︎ ☝︎⚐︎⚐︎👎︎ ☝︎✋︎✞︎☜︎ 💣︎☜︎ ✌︎ ❄︎✌︎💧︎❄︎✡︎ 👌︎✋︎❄︎☜︎", Random.Range(1, 9)));
            Debug.LogError(message);
        }
        communicationProgress++;   
    }
    
    void Kill(float distance)
    {
        if (distance < killRange)
        {
            audioSource.volume = 1;
            transform.localScale *= 2.5f;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    void HijackAudio()
    {
        var audioSources = FindObjectsOfType<AudioSource>().Where(source => source != audioSource);
        foreach (var source in audioSources)
        {
            source.clip = interferenceSound;
        }
    }
}

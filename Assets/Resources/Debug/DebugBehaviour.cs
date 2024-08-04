using UnityEngine;

public class DebugBehaviour : MonoBehaviour
{
    public GameObject debug;
    
    // Start is called before the first frame update
    void Start()
    {
        if (FlipCoin() && FlipCoin() && FlipCoin() && FlipCoin())
        {
            SpawnDebugObject();
        }
    }

    bool FlipCoin()
    {
        return Random.Range(0, 2) == 0;
    }
    
    float RandomDist()
    {
        var baseNumber = Random.Range(20f, 25f);
        baseNumber *= Random.Range(0, 2) == 0 ? -1 : 1;
    
        return baseNumber;
    }
    
    // Update is called once per frame
    void SpawnDebugObject()
    {
        Debug.LogWarning("");
        var obj = Instantiate(debug);
        var playerPosition = FindObjectOfType<Dola>().transform.position;
        var position = new Vector3(playerPosition.x + RandomDist(), Mathf.Abs(playerPosition.y + RandomDist()), playerPosition.z);
        obj.transform.position = position;
    }
}

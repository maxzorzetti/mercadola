using UnityEngine;

public class Dola : MonoBehaviour
{
    public void Amaze()
    {
        GetComponentInChildren<Animator>().SetTrigger("Amazed");
    }
}
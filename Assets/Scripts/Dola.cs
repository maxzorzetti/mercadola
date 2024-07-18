using UnityEngine;

public class Dola : MonoBehaviour
{
    public void Amaze(bool isAmazed = true)
    {
        if (isAmazed)
        {
            GetComponentInChildren<Animator>().SetTrigger("Amazed");
        }
        else
        {
            GetComponentInChildren<Animator>().ResetTrigger("Amazed");
        }
    }
}
#nullable enable
using UnityEngine;

// TODO: Do composable effects using monads!!!!!! 

public abstract class Effect : ScriptableObject
{
    public abstract void Apply(GameObject target, GameObject? source = null);

    public void Apply(MonoBehaviour target, MonoBehaviour? source = null)
    {
        // IDE is asking me to write like this instead of using `source?.gameObject`
        // due to the conditional access operator messing with underlying Unity code
        if (source == null)
        {
            Apply(target.gameObject);
        }
        else
        {
            Apply(target.gameObject, source.gameObject);
        }
    }
}
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float SpinTime = 0.5f;
    
    public bool SpinOnStart;
    public bool AutoSpin;
    public AnimationCurve SpinEasing = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    Progression spinProgression;
    bool isSpinning;
    float spinSpeed => 1 / SpinTime;

    readonly AnimationCurve linearCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField] public Transform transformTarget;

    void Start()
    {
        isSpinning = SpinOnStart;
        spinProgression = new Progression();

        if (transformTarget == null)
        {
            var spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
            transformTarget = spriteTransform != null ? spriteTransform : transform; 
        }
    }

    public void Spin() => isSpinning = true;
    
    public void StopSpin(bool reset = true)
    {
        isSpinning = false;
        if (reset) ResetSpin();
    }

    void Update()
    {
        if (isSpinning)
        {
            SpinLogic();
        }
        else
        {
            ResetSpin();
        }
    }

    void SpinLogic()
    {
        // Use different easing if AutoSpin is enabled
        var spin = AutoSpin
            ? linearCurve.Evaluate(spinProgression.ProgressRate) * 360
            : SpinEasing.Evaluate(spinProgression.ProgressRate) * 360;
        
        // Apply rotation
        transformTarget.rotation = Quaternion.Euler(0, 0, -spin);
            
        // Continue the spin and check if it's done
        spinProgression.Advance(Time.deltaTime * spinSpeed);
        if (spinProgression.Consume())
        {
            // Continue spinning if AutoSpin is enabled
            if (AutoSpin) return;
            
            // Stop spinning
            isSpinning = false;
            ResetSpin();
        }
    }

    public void ResetSpin()
    {
        spinProgression.Reset();
        transformTarget.rotation = Quaternion.Euler( 0, 0, 0);
    }
}
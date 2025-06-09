using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GhostData", menuName = "Scriptable Objects/GhostData")]
public class GhostData : ScriptableObject
{

    [SerializeField] private float InvincibilityTimer;
    private float visibilityValue = 0;

    [System.NonSerialized] public UnityEvent<float> reveal;
    [System.NonSerialized] public UnityEvent caught;
    [System.NonSerialized] public UnityEvent leftLightEvent;

    private void OnEnable()
    {
        visibilityValue = 0;

        reveal = new UnityEvent<float>();
        caught = new UnityEvent();
        leftLightEvent = new UnityEvent();
    }


    public void RevealSignal(float power)
    {
        visibilityValue += power;
        Mathf.Clamp01(visibilityValue);
        
        reveal.Invoke(visibilityValue);
    }

    public void LeftLight()
    {
        leftLightEvent.Invoke();
    }

    public void CaughtSignal()
    {
        caught.Invoke();
    }

}

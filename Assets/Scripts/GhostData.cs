using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GhostData", menuName = "Scriptable Objects/GhostData")]
public class GhostData : ScriptableObject
{

    [SerializeField] public float InvincibilityTimer;
    [SerializeField] public float TimeToRespawn;
    [SerializeField] public AudioClip GhostLaugh;
    private float visibilityValue = 0;

    [System.NonSerialized] public UnityEvent<float> revealEvent;
    [System.NonSerialized] public UnityEvent caughtEvent;
    [System.NonSerialized] public UnityEvent<bool> underLightEvent;

    private void OnEnable()
    {
        visibilityValue = 0;

        revealEvent = new UnityEvent<float>();
        caughtEvent = new UnityEvent();
        underLightEvent = new UnityEvent<bool>();
    }


    public void RevealSignal(float power)
    {

        visibilityValue += power;

        visibilityValue = Mathf.Clamp01(visibilityValue);


        revealEvent.Invoke(visibilityValue);
    }

    public void SetLightSignal(bool status)
    {
        underLightEvent.Invoke(status);
    }

    public void CaughtSignal()
    {
        caughtEvent.Invoke();
        visibilityValue = 0;
    }

}

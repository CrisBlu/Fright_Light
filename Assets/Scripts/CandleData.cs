using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CandleData", menuName = "Scriptable Objects/CandleData")]
public class CandleData : ScriptableObject
{
    [SerializeField] private float MaxHP;
    [SerializeField] GameData gameData;
    private float currentHP = 0;


    [System.NonSerialized] public UnityEvent<float> candleDamageEvent;
    [System.NonSerialized] public UnityEvent<bool> detectGhostEvent;
    [System.NonSerialized] public UnityEvent deathEvent;
    private bool dead = false;

    private void OnEnable()
    {
        currentHP = MaxHP;
        dead = false;
        candleDamageEvent = new UnityEvent<float>();
        detectGhostEvent = new UnityEvent<bool>();
        deathEvent = new UnityEvent();
    }


    public void onDamaged(float damage)
    {
        if (dead) return;
        currentHP -= damage;
        candleDamageEvent.Invoke(currentHP);

        if (currentHP <= 0 )
        {
            
            dead = true;
            onDeath();
        }
    }

    public void onDeath()
    {
        gameData.PointToGhost();
        deathEvent.Invoke();
    }

    public void onDetectGhost(bool detectGhost)
    {
        detectGhostEvent.Invoke(detectGhost);
    }

    

}

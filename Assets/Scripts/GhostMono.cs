using StarterAssets;
using System.Collections;
using UnityEngine;

public class GhostMono : MonoBehaviour
{
    public GhostData GhostData;
    private ThirdPersonController controller;
    private Vector3 startingPos;
    private CandleData targetCandle;
    private AudioSource audioSource;

    private void Start()
    {
        controller = GetComponent<ThirdPersonController>();
        startingPos = transform.position;
        audioSource = GetComponent<AudioSource>();

        GhostData.caughtEvent.AddListener(OnCaught);
    }


    private void OnCaught()
    {
        
        if (targetCandle != null)
        {
            targetCandle.onDetectGhost(false);
            targetCandle = null;
        }
        StartCoroutine("Died");
    }

    IEnumerator Died()
    {
        
        float currentSpeed = controller.MoveSpeed;
        controller.MoveSpeed = 0;

        yield return new WaitForSeconds(GhostData.TimeToRespawn);

        controller.MoveSpeed = currentSpeed;
        

        Respawn();

        
    }

    private void Respawn()
    {
        transform.position = startingPos;
        GhostData.SetLightSignal(false);
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candle"))
        {
            audioSource.PlayOneShot(GhostData.GhostLaugh);
            targetCandle = other.GetComponent<CandleMono>().CandleData;
            targetCandle.onDetectGhost(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (targetCandle != null)
        {
            GhostData.SetLightSignal(true);
            GhostData.RevealSignal(.001f);

            targetCandle.onDamaged(Time.deltaTime * 8);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Candle"))
        {
            if(targetCandle != null)
            {
                GhostData.SetLightSignal(false);
                targetCandle.onDetectGhost(false);
                targetCandle = null;
            }
            
        }
    }

    //Ghost can now extinguish lights and it basically works how I want it to; todo still, Game needs to be over when 3 of the lights extinguish, or when the ghost is caught 3 times
    //furniture and room; background noise (rain); sound effects for flashlight, sound effects for a ghost capturing a light; ghost powers, levitate, push, possess

    //Unsure if I can do super advanced work right now but I probably can create and place furniture around



}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightMono : MonoBehaviour
{

    [SerializeField] private InputActionAsset VRInput;
    [SerializeField] private float revealingPower;
    [SerializeField] private GameData gameData;
    private InputAction flash;
    private GhostData ghostInHeadLights;
    private Light flashlightLight;
    private AudioSource audioSource;
    bool canFlash = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        flashlightLight = GetComponentInChildren<Light>();
        flash = VRInput.FindActionMap("XRI Right Interaction").FindAction("Activate");
        flash.performed += OnFlash;

        gameData.gameOverEvent.AddListener(onGameOver);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            ghostInHeadLights = other.gameObject.GetComponent<GhostMono>().GhostData;
            if (ghostInHeadLights != null)
            {
                ghostInHeadLights.SetLightSignal(true);

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(ghostInHeadLights != null)
        {
            ghostInHeadLights.RevealSignal(revealingPower);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Ghost"))
        {
            if (ghostInHeadLights == null)
            {
                return;
            }

            Debug.Log("Ghost Exited");
            ghostInHeadLights.SetLightSignal(false);
            ghostInHeadLights = null;
        }
    }


    private void OnFlash(InputAction.CallbackContext obj)
    {
        //Make coroutine, set intensity to a million, then slowly decrease it back to base
        if(!canFlash)
        {
            return;
        }
        canFlash = false;
        audioSource.Play();
        StartCoroutine("RecoveryFromFlash");

        if(ghostInHeadLights != null) 
        {
            ghostInHeadLights.CaughtSignal();
            ghostInHeadLights = null;
        }
    }

    IEnumerator RecoveryFromFlash()
    {
        float originalIntense = flashlightLight.intensity;
        float originalRevealingPower = revealingPower;
        flashlightLight.intensity *= 10;

        yield return new WaitForSeconds(1f);

        flashlightLight.intensity = flashlightLight.intensity * .0001f;
        revealingPower = 0;

        yield return new WaitForSeconds(4f);

        flashlightLight.intensity = originalIntense;
        revealingPower = originalRevealingPower;

        canFlash = true;
        
    }

    void onGameOver(bool winner)
    {
        Debug.Log("Game is over " + winner);
    }

}

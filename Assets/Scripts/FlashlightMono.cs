using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightMono : MonoBehaviour
{

    [SerializeField] private InputActionAsset VRInput;
    [SerializeField] private float revealingPower;
    private InputAction flash;
    private GhostData ghostInHeadLights;

    private void Start()
    {
        flash = VRInput.FindActionMap("XRI Right Interaction").FindAction("Activate");
        flash.performed += OnFlash;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            Debug.Log("Ghost within!");
            ghostInHeadLights = other.gameObject.GetComponent<GhostMono>().GhostData;
            if (ghostInHeadLights != null)
            {
                ghostInHeadLights.RevealSignal(revealingPower);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            ghostInHeadLights.LeftLight();
            ghostInHeadLights = null;
        }
    }


    private void OnFlash(InputAction.CallbackContext obj)
    {
        if(ghostInHeadLights != null) 
        {
            ghostInHeadLights.CaughtSignal();   
        }
    }

}

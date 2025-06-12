using UnityEngine;

public class CandleMono : MonoBehaviour
{
    [SerializeField] public CandleData CandleData;
    private Light candleLight;
    private Color candleColor;
    private AudioSource audioSource;

    private void Start()
    {
        candleLight = GetComponentInChildren<Light>();
        CandleData.candleDamageEvent.AddListener(DimLightOnCandleDamaged);
        CandleData.detectGhostEvent.AddListener(TurnBlueGhostDetected);
        candleColor = candleLight.color;
        audioSource = GetComponent<AudioSource>();

    }


    private void DimLightOnCandleDamaged(float currentHealth)
    {
        //Works only if the max light intensity is equal to the max health; 25 seconds
        candleLight.intensity = currentHealth;
    }

    private void TurnBlueGhostDetected(bool ghostDetected)
    {
        if (ghostDetected)
        {
            audioSource.Play();
            candleLight.color = Color.blue;
        }
        else
        {
            audioSource.Stop();
            candleLight.color = candleColor;
        }

    }
    //On death, stop noises and get rid of trigger


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            ghostWithin = other.gameObject.GetComponent<GhostMono>().GhostData;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(ghostWithin != null)
        {
            CandleData.onDamaged(Time.deltaTime);
            //Can
        }
    }*/


}

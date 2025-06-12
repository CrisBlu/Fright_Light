using UnityEngine;

public class CandleLightMono : MonoBehaviour
{
    [SerializeField] CandleData CandleData;
    private Light candleLight;
    private Color candleColor;

    private void Start()
    {
        candleLight = GetComponent<Light>();
        CandleData.candleDamageEvent.AddListener(DimLightOnCandleDamaged);
        CandleData.detectGhostEvent.AddListener(TurnBlueGhostDetected);
        candleColor = candleLight.color;

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
            candleLight.color = Color.blue;
        }
        else
        {
            candleLight.color = candleColor;
        }
          
    }




}

using UnityEngine;

public class TurnOnLights : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject LoseScreen;
    private Light myLight;

    private void Start()
    {
        gameData.gameOverEvent.AddListener(EndGame);
        myLight = GetComponent<Light>();
    }


    void EndGame(bool winner)
    {
        if (winner)
        {
            myLight.intensity = 5;
        }
        else
        {
            myLight.intensity = 0;
            LoseScreen.SetActive(true);
        }
    }
}

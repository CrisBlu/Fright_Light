using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private CandleData CandleDataOne;
    [SerializeField] private CandleData CandleDataTwo;
    [SerializeField] private CandleData CandleDataThree;
    [SerializeField] private CandleData CandleDataFour;
    [SerializeField] private GhostData GhostData;
    [System.NonSerialized] public UnityEvent<bool> gameOverEvent;

    //True if player wins, False if Ghost wins

    private int ghostPoints;
    private int hunterPoints;



    private void OnEnable()
    {
        gameOverEvent = new UnityEvent<bool>();
        ghostPoints = 0;
        hunterPoints = 0;

        /*//CandleDataOne.deathEvent.AddListener(PointToGhost);
        CandleDataTwo.deathEvent.AddListener(PointToGhost);
        //CandleDataThree.deathEvent.AddListener(PointToGhost);
        //andleDataFour.deathEvent.AddListener(PointToGhost);*/



        GhostData.caughtEvent.AddListener(PointToHunter);
    }

    public void PointToGhost()
    {
        Debug.Log("Point to ghost");
        ghostPoints++;
        if (ghostPoints >= 3)
        {
            gameOverEvent.Invoke(false);
        }
    }

    public void PointToHunter()
    {
        Debug.Log("Point to hunter");
        hunterPoints++;

        if (hunterPoints >= 3)
        {
            gameOverEvent.Invoke(true);
        }
    }
}

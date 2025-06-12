using UnityEngine;

public class MakeGhostVisibleMono : MonoBehaviour
{
    [SerializeField] private GhostData GhostData;
    private SpriteRenderer spriteRenderer;
    private bool underLight = false;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GhostData.revealEvent.AddListener(ChangeSpriteOpacity);
        GhostData.underLightEvent.AddListener(SetUnderLight);
        GhostData.caughtEvent.AddListener(CaughtColorChange);
    }

    private void Update()
    {
        if (!underLight)
        {
            GhostData.RevealSignal(-.05f);
        }


    }

    private void SetUnderLight(bool lightStatus)
    {
        underLight = lightStatus;
    }



    private void ChangeSpriteOpacity(float opacity)
    {

        Mathf.Clamp01(opacity);
        Color color = Color.white;
        color.a = opacity;
        spriteRenderer.color = color;

    }

    private void CaughtColorChange()
    {
        ChangeSpriteOpacity(-999f);
    }

}

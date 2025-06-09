using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class MakeGhostVisibleMono : MonoBehaviour
{
    [SerializeField] private GhostData GhostData;
    private SpriteRenderer spriteRenderer;
    private bool underLight = false;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GhostData.reveal.AddListener(ChangeSpriteOpacity);
        GhostData.leftLightEvent.AddListener(NoLongerUnderLight);
    }

    private void Update()
    {
        if (!underLight)
        {
            GhostData.RevealSignal(-.05f);
        }
    }

    private void NoLongerUnderLight()
    {
        underLight = false;
    }


    private void ChangeSpriteOpacity(float opacity)
    {
        if(opacity > 0f)
        {
            underLight = true;
        }

        Mathf.Clamp01(opacity);
        Color color = Color.white;
        color.a = opacity;
        spriteRenderer.color = color;

    }

}

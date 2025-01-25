using UnityEngine;

public class SetRenderTextureResolution : MonoBehaviour
{
    public Camera Target;
    public RenderTexture RenderTexture;
    public float Multiplier = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RenderTexture.Release();
        RenderTexture.width = (int)(Screen.width * Multiplier);
        RenderTexture.height = (int)(Screen.height * Multiplier);
        Target.targetTexture = null;
        Target.targetTexture = RenderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

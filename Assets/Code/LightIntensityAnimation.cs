using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightIntensityAnimation : MonoBehaviour
{
    public Vector2 IntensityRange = new Vector2(0.8f, 1.2f);
    public float ChangeSpeed = 1f;
    public Light2D Light;

    private float _pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pos = Random.Range(0f, 100f);
        if(Light == null)
            Light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _pos += Time.deltaTime * ChangeSpeed;
        var strength = math.remap(0f, 1f, IntensityRange.x, IntensityRange.y, Mathf.PerlinNoise1D(_pos));
        Light.intensity = strength;
    }
}

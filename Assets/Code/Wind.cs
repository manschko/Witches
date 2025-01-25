using UnityEngine;

public class Wind : MonoBehaviour
{
    public static float CurrentWindStrength;
    public static float CurrentWindDirection;
    public static float WindTime;

    public float WindStrengthReadout;
    public float WindDirectionReadout;

    public float StrengthChangeSpeed = 1f;
    public float Strength = 1f;

    public float DirectionChangeSpeed = 0.5f;
    public float DirectionStrength = 1f;

    private float _xs, _ys;
    private float _xd, _yd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _xs = Random.Range(0f, 100f);
        _ys = Random.Range(0f, 100f);
        _xd = Random.Range(0f, 100f);
        _yd = Random.Range(0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        WindTime += WindTime;
        _xs += Time.deltaTime * StrengthChangeSpeed;
        _ys += Time.deltaTime * StrengthChangeSpeed;

        _xd += Time.deltaTime * DirectionChangeSpeed;
        _yd += Time.deltaTime * DirectionChangeSpeed;

        var windStrength = Mathf.PerlinNoise(_xs, _ys) * Strength;
        CurrentWindStrength = WindStrengthReadout = windStrength;

        var windDirection = Mathf.PerlinNoise(_xd, _yd) * DirectionStrength;
        WindDirectionReadout = windDirection;
        CurrentWindDirection = (WindDirectionReadout - 0.5f) * 2;
    }

    private void OnDrawGizmos()
    {
        var strengthStart = new Vector3(5f, 0f);
        var strengthEnd = new Vector3(5f, 3f);

        Gizmos.DrawLine(strengthStart, strengthEnd);
        Gizmos.DrawSphere(Vector3.Lerp(strengthStart, strengthEnd, WindStrengthReadout / Strength), 0.2f);

        var directionStart = new Vector3(5f - 1.5f, 1.5f);
        var directionEnd = new Vector3(5f + 1.5f, 1.5f);

        Gizmos.DrawLine(directionStart, directionEnd);
        Gizmos.DrawSphere(Vector3.Lerp(directionStart, directionEnd, WindDirectionReadout / DirectionStrength), 0.2f);
    }
}

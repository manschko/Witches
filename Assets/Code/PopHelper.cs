using UnityEngine;

[DefaultExecutionOrder(50)]
public class PopHelper : MonoBehaviour
{
    private static PopHelper _instance;

    private void Start()
    {
        _instance = this;
    }

    public static float MultiDelay;
    public float PerPopDelay = 0.1f;

    private void LateUpdate()
    {
        MultiDelay = 0f;
    }

    public static void RegisterPop()
    {
        MultiDelay += _instance.PerPopDelay;
    }
}

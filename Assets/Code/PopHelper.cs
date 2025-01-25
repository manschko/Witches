using System.Collections.Generic;
using System.Linq;
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

    private List<Bubble> _bubbles = new List<Bubble>();
    private bool _didHit;

    private void LateUpdate()
    {
        MultiDelay = 0f;

        if(_didHit == false && _bubbles.Count > 0)
        {
            HandleMiss();
        }

        _bubbles.Clear();
        _didHit = false;
    }

    public static void RegisterPop()
    {
        MultiDelay += _instance.PerPopDelay;
    }

    public static void RegisterNonClick(Bubble bubble)
    {
        _instance._bubbles.Add(bubble);
    }

    public static void RegisterClick()
    {
        _instance._didHit = true;
    }

    private void HandleMiss()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var pos = ray.origin;
        //_bubbles.Select(x => x.)
    }
}

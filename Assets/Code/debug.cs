using UnityEngine;

public class debug : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Color gizmoColor = Color.yellow;
    public Vector3 gizmoSize = Vector3.one;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}

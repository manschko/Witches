using System;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    public string Name;
    public Sprite DisplaySprite;


    private Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    [Range(0.0f, 100.0f)] public float damping = 1.0f;

    [Range(0.0f, 100.0f)] public float frequency = 5.0f;

    public bool debug = true;
    public Color debugColor = Color.cyan;

    private TargetJoint2D joint;

    void OnMouseDown()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);


        // Add a target joint to the Rigidbody2D GameObject.
        joint = rb.gameObject.AddComponent<TargetJoint2D>();
        joint.dampingRatio = damping;
        joint.frequency = frequency;

        // Attach the anchor to the local-point where we clicked.
        joint.anchor = joint.transform.InverseTransformPoint(mousePosition);
    }

    void OnMouseUp()
    {
        Destroy(joint);
        joint = null;
    }

    void OnMouseDrag()
    {
        if (!joint)
            return;

        var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        joint.target = worldPos;

        if (debug)
            Debug.DrawLine(joint.transform.TransformPoint(joint.anchor), worldPos, debugColor);
    }
}
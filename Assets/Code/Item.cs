using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    public string Name;
    public Sprite DisplaySprite;
    public float snapThresholdScale = 2.0f;

    public Vector3 size = new Vector3(10,10,0);


    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CircleCollider2D colider;
    private Camera mainCamera;

    public Boolean placed;
    [HideInInspector] 
    public Boolean dragged;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        colider = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;
        if (placed)
        {
            colider.radius *= snapThresholdScale;
            colider.isTrigger = true;
            sr.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
        }
        MatchParentSize();
    }

    [Range(0.0f, 100.0f)] public float damping = 1.0f;
    [Range(0.0f, 100.0f)] public float frequency = 5.0f;

    public bool debug = true;
    public Color debugColor = Color.cyan;
    private GameObject snapObject;
    private TargetJoint2D joint;

    void OnMouseDown()
    {
        if (placed)
        {
            return;
        }
        if(snapObject){
            rb.bodyType = RigidbodyType2D.Dynamic;
            snapObject.SetActive(true);

        }
        dragged = true;
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);


        joint = rb.gameObject.AddComponent<TargetJoint2D>();
        joint.dampingRatio = damping;
        joint.frequency = frequency;

        joint.anchor = joint.transform.InverseTransformPoint(mousePosition);
    }

    void OnMouseUp()
    {
        if (placed)
        {
            return;
        }

        Destroy(joint);
        joint = null;
        // Snap to placement Position
        dragged = false;
        if (snapObject != null)
        {
            transform.position = snapObject.transform.position;
            transform.rotation = snapObject.transform.rotation; 
            rb.bodyType = RigidbodyType2D.Static;
            sr.enabled = true;
            snapObject.SetActive(false);
            
        }
        
    }

    void OnMouseDrag()
    {
        if (placed || !joint)
        {
            return;
        }

        var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        joint.target = worldPos;


        if (debug)
            Debug.DrawLine(joint.transform.TransformPoint(joint.anchor), worldPos, debugColor);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Vermeiden von trigger whenn nich dragged und vom plazierten Object
        if (placed || !dragged)
        {
            return;
        }
        other.GetComponent<SpriteRenderer>().enabled = true;
        sr.enabled = false; 


        snapObject = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Vermeiden von trigger whenn nich dragged und vom plazierten Object
        if (placed || !dragged)
        {
            return;
        }
        other.GetComponent<SpriteRenderer>().enabled = false;
        sr.enabled = true; 
        snapObject = null;
    }

    void MatchParentSize()
    {
        Vector3 parentSize = size;
        float x = parentSize.x / sr.sprite.bounds.size.x;
        // Scale the sprite to match parent's size
        sr.gameObject.transform.localScale = new Vector3(
            x,
            (parentSize.y / sr.sprite.bounds.size.y),
            1f
        );
        colider.radius = (size.x * (1/x)) / 2;
    }
}
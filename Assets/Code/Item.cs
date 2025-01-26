using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

[Serializable]
public class Item : MonoBehaviour
{
    public GameObject FullLight;
    public GameObject InPotLight;
    public string Name;
    public Sprite DisplaySprite;
    public float snapThresholdScale = 2.0f;

    public Vector3 size = new Vector3(10,10,0);
    public SpriteRenderer spriteRenderer;
    public FMODUnity.EventReference DropEvent;


    private Rigidbody2D rb;
    private CircleCollider2D colider;
    private Camera mainCamera;

    public Boolean placed;
    [HideInInspector] 
    public Boolean dragged;
    public Light2D Light;

    private Collider2D trigger;
    private Collider2D oldTrigger;


    void Start()
    {
        if(DisplaySprite){
            spriteRenderer.sprite = DisplaySprite;
        }
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;
        if (placed)
        {
            Light.enabled = false;
            colider.isTrigger = true;
            spriteRenderer.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
        }
        MatchParentSize();
        Light.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 0.5f), 1f);
    }
    
    void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = debugColor;
            Gizmos.DrawWireSphere(colider.transform.position, colider.radius);
        }
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
            colider.isTrigger = false;
            snapObject.SetActive(true);
            spriteRenderer.sortingOrder = 10;
            colider.radius = size.x/3;
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
            colider.radius = size.x/2;
            colider.isTrigger = true;
            spriteRenderer.sortingOrder = 9;
            spriteRenderer.enabled = true;
            snapObject.SetActive(false);

            FMODUnity.RuntimeManager.PlayOneShot(DropEvent, transform.position);
        }
        
    }

    void OnMouseDrag()
    {
        if (placed || !joint)
        {
            return;
        }
        var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (trigger)
        {
            transform.position = new Vector3(worldPos.x , worldPos.y, transform.position.z);
        }
        else
        {
            
            joint.target = worldPos;
            if (debug)
                Debug.DrawLine(joint.transform.TransformPoint(joint.anchor), worldPos, debugColor);
        }
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Vermeiden von trigger whenn nich dragged und vom plazierten Object
        if (placed || !dragged || other.CompareTag("PotHole"))
        {
            return;
        }
        if (!other.GetComponent<Item>().placed)
        {
            return;
        }
        if(trigger)
        {

            oldTrigger = trigger;    
            trigger.GetComponent<Item>().spriteRenderer.enabled = false;
        }
        trigger = other;
        SpriteRenderer otherSpriteRenderer = other.GetComponent<Item>().spriteRenderer;
        otherSpriteRenderer.sprite = DisplaySprite;
        otherSpriteRenderer.transform.localScale = spriteRenderer.transform.localScale;
    

        otherSpriteRenderer.enabled = true;

        //todo mimic sprite from drag item
        spriteRenderer.enabled = false; 


        snapObject = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Vermeiden von trigger whenn nich dragged und vom plazierten Object
        if (placed || !dragged || trigger != other )
        {
            if(oldTrigger == other){
                oldTrigger = null;
            }
            return;
        }
        other.GetComponent<Item>().spriteRenderer.enabled = false;
        if(oldTrigger != null){
            if(oldTrigger.IsTouching(colider)){
                SpriteRenderer otherSpriteRenderer = oldTrigger.GetComponent<Item>().spriteRenderer;
                otherSpriteRenderer.sprite = DisplaySprite;
                otherSpriteRenderer.transform.localScale = spriteRenderer.transform.localScale;
                otherSpriteRenderer.enabled = true;
                trigger = oldTrigger;
                oldTrigger = null;
                return;
            }
        }
        spriteRenderer.enabled = true; 
        snapObject = null;
        trigger = null;
    }

    void MatchParentSize()
    {
        Vector3 parentSize = size;
        float x = parentSize.x / spriteRenderer.sprite.bounds.size.x;
        // Scale the sprite to match parent's size
        spriteRenderer.gameObject.transform.localScale = new Vector3(
            x,
            (parentSize.y / spriteRenderer.sprite.bounds.size.y),
            1f
        );
        if(placed){
            colider.radius = size.x / 2;
        }else {
            colider.radius = size.x / 3;
        }
        
    }
}
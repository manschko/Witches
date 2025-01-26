using System;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Collider2D leftCollider;
    public Collider2D rightCollider;
    public Collider2D killCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Collider2D collider2D;
    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        leftCollider.enabled = true;
        rightCollider.enabled = true;
        killCollider.enabled = true;
        other.GetComponent<Item>().spriteRenderer.sortingOrder = -2;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        leftCollider.enabled = false;
        rightCollider.enabled = false;
        killCollider.enabled = false;
        collider2D.enabled = false;
        other.GetComponent<Item>().spriteRenderer.sortingOrder = 10;
    }
}

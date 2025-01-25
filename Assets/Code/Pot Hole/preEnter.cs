using UnityEngine;

public class preEnter : MonoBehaviour
{
    public Collider2D potCollider;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Item>().dragged)
        {
            return;
        }
        potCollider.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(potCollider.IsTouching(other))
        {
            return;
        }
        potCollider.enabled = false;
    }

}

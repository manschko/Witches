using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Collider2D leftCollider;
    public Collider2D rightCollider;
    public Collider2D killCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider2D ownCollider;

    private string ItemLayerInPot = "ItemInPot";
    private string ItemLayer = "PotItems";

    private void Start()
    {
        ownCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        leftCollider.enabled = true;
        rightCollider.enabled = true;
        killCollider.enabled = true;

        other.GetComponent<Item>().spriteRenderer.sortingLayerName = ItemLayerInPot;
        Item item = other.GetComponent<Item>();
        item.FullLight.SetActive(false);
        item.InPotLight.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        leftCollider.enabled = false;
        rightCollider.enabled = false;
        killCollider.enabled = false;
        ownCollider.enabled = false;
        
        other.GetComponent<Item>().spriteRenderer.sortingLayerName = ItemLayer;
        Item item = other.GetComponent<Item>();
        item.FullLight.SetActive(true);
        item.InPotLight.SetActive(false);
    }
}
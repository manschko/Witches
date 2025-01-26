using UnityEngine;

public class spawnItems : MonoBehaviour
{
    public Item item;
    public int itemCount = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 scale = transform.localScale;
        float sectionsSize = scale.x / itemCount;
        float yOffset = item.size.x / 2;
        item.placed = true;
        for (int i = 0; i < itemCount; i++)
        {
            Vector3 position = new Vector3(transform.position.x - scale.x / 2 + sectionsSize * i + sectionsSize / 2, transform.position.y + yOffset, transform.position.z);
            GameObject newItem = Instantiate(item.gameObject, position, Quaternion.identity);
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

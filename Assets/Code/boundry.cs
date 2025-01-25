using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class boundry : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 bottomLeft;
    public Vector3 topRight;

    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
        Vector3 middle = new Vector3(
        (bottomLeft.x + topRight.x)/2,
        (bottomLeft.y + topRight.y)/2,
        0);
        transform.position = middle;
        
        Vector3 size = new Vector3(
            math.abs(bottomLeft.x) + math.abs(topRight.x),
            math.abs(bottomLeft.y) + math.abs(topRight.y),
            0
            );
        transform.localScale = size;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

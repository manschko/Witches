using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Bubble : MonoBehaviour
{
    public Item ContainedItem;
    public UnityEvent OnPopped = new UnityEvent();
    private float x, y;
    public SpriteRenderer Renderer;
    public float Speed = 4f;
    public Vector2 RandomSpeedMultiplier = new Vector2(0.9f, 1.1f);
    public Vector2 RandomSize = new Vector2(0.5f, 2f);

    private void Start()
    {
        //y = Random.Range(0, 100000);
        Speed *= Random.Range(RandomSpeedMultiplier.x, RandomSpeedMultiplier.y);
        transform.localScale *= Random.Range(RandomSize.x, RandomSize.y);
    }

    private void Update()
    {
        x += Time.deltaTime;

        var add = Mathf.PerlinNoise(x, y);
        var pos = transform.position;
        pos.y += add * Speed * Time.deltaTime;
        transform.position = pos;

        if (!Renderer.isVisible || IsBeingClicked()) Pop();
    }

    private bool IsBeingClicked()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var results = Physics2D.RaycastAll(ray.origin, ray.direction);
        if (results.Any(x => x.collider.gameObject == gameObject) && Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }

    public void Pop()
    {
        OnPopped.Invoke();
        Destroy(gameObject);
    }
}

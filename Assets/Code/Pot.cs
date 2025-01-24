using UnityEngine;
using UnityEngine.Events;

public class Pot : MonoBehaviour
{
    public static UnityEvent<Bubble> OnBubbleSpawned = new UnityEvent<Bubble>();
    public float SpawnTimer = 1f;
    public GameObject PotPrefab;
    public BoxCollider2D SpawnArea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnDelay = SpawnTimer;
    }

    private float _spawnDelay = 0f;

    // Update is called once per frame
    void Update()
    {
        _spawnDelay -= Time.deltaTime;
        if(_spawnDelay <= 0f)
        {
            _spawnDelay = SpawnTimer;
            SpawnBubble();
        }
    }

    public void SpawnBubble()
    {
        var instance = Instantiate(PotPrefab);
        instance.transform.position = GetRandomSpawnLocation();
        OnBubbleSpawned.Invoke(instance.GetComponent<Bubble>());
    }

    private Vector2 GetRandomSpawnLocation()
    {
        var bounds = SpawnArea.bounds;
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }
}

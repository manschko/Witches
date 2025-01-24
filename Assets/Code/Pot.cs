using UnityEngine;
using UnityEngine.Events;

public class Pot : MonoBehaviour
{
    public static UnityEvent<Bubble> OnBubbleSpawned = new UnityEvent<Bubble>();
    public float SpawnTimer = 1f;
    public GameObject PotPrefab;

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
        OnBubbleSpawned.Invoke(instance.GetComponent<Bubble>());
    }
}

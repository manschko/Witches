using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pot : MonoBehaviour
{
    public static UnityEvent<Bubble> OnBubbleSpawned = new UnityEvent<Bubble>();
    public static List<Bubble> AllBubbles = new List<Bubble>();
    public float SpawnTimer = 1f;
    public Vector2 SpawnDeviation = new Vector2(-0.5f, 1f);
    public GameObject PotPrefab;
    public BoxCollider2D SpawnArea;
    public FMODUnity.EventReference MusicEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnDelay = GetSpawnDelay();

        FMODUnity.RuntimeManager.PlayOneShot(MusicEvent, transform.position);
    }

    private float _spawnDelay = 0f;

    // Update is called once per frame
    void Update()
    {
        _spawnDelay -= Time.deltaTime;
        if (_spawnDelay <= 0f)
        {
            _spawnDelay = GetSpawnDelay();
            SpawnBubble();
        }
    }

    private float GetSpawnDelay()
    {
        return Random.Range(SpawnDeviation.x, SpawnDeviation.y) + SpawnTimer;
    }        

    public void SpawnBubble()
    {
        var instance = Instantiate(PotPrefab);
        instance.transform.position = GetRandomSpawnLocation();
        var bubble = instance.GetComponent<Bubble>();
        OnBubbleSpawned.Invoke(bubble);
        AllBubbles.Add(bubble);
    }

    private Vector2 GetRandomSpawnLocation()
    {
        var bounds = SpawnArea.bounds;
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }
}

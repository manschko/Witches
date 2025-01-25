using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class Bubble : MonoBehaviour
{
    public Item ContainedItem;
    public UnityEvent OnPopped = new UnityEvent();
    public SpriteRenderer Renderer;
    public CinemachineImpulseSource Impulse;
    public float Speed = 4f;
    public Vector2 RandomSpeedMultiplier = new Vector2(0.9f, 1.1f);
    public Vector2 RandomSize = new Vector2(0.5f, 2f);
    public Animator Animator;
    public float VelocityDecay = 1f;
    public GameObject PopParticlePrefab;
    public AudioClip PopSound;
    public float PopSoundVolume = 1f;

    private float _aliveTime;
    private float _x, _y;
    private bool _isPopped;
    private Vector2 _popVelocity;

    private void Start()
    {
        //y = Random.Range(0, 100000);
        Speed *= Random.Range(RandomSpeedMultiplier.x, RandomSpeedMultiplier.y);
        transform.localScale *= Random.Range(RandomSize.x, RandomSize.y);
        //Renderer.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.0f, 0.3f), 1f);
    }

    private void Update()
    {
        if (_isPopped) return;

        _x += Time.deltaTime;
        _aliveTime += Time.deltaTime / 4f;

        var add = Mathf.PerlinNoise(_x, _y);
        var pos = transform.position;
        pos.y += add * Speed * Time.deltaTime + (_popVelocity.y * Time.deltaTime);
        pos.x += Mathf.PerlinNoise1D(transform.position.y + Wind.WindTime) * Time.deltaTime *
            Wind.CurrentWindStrength * Wind.CurrentWindDirection * _aliveTime * transform.localScale.x
             + (_popVelocity.x * Time.deltaTime);
        //pos.x += ((Wind.CurrentWindDirection - 0.5f) * 2) * Wind.CurrentWindStrength * Time.deltaTime * _aliveTime;
        transform.position = pos;

        if (!Renderer.isVisible)
        {
            Pot.AllBubbles.Remove(this);
            Destroy(gameObject);
        }

        if(IsBeingClicked()) Pop(false);

        _popVelocity.x = Mathf.SmoothDamp(_popVelocity.x, 0f, ref _velocityStopX, VelocityDecay);
        _popVelocity.y = Mathf.SmoothDamp(_popVelocity.y, 0f, ref _velocityStopY, VelocityDecay);
    }

    private float _velocityStopX, _velocityStopY;

    private bool IsBeingClicked()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var results = Physics2D.OverlapPointAll(ray.origin);
        if (results.Any(x => x.gameObject == gameObject) && Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }

    public void Pop(bool silent)
    {
        OnPopped.Invoke();
        Destroy(gameObject, 1f);
        OnPopped.RemoveAllListeners();
        _isPopped = true;
        Impulse.GenerateImpulse(0.13f);
        Animator.SetTrigger("Pop");
        Pot.AllBubbles.Remove(this);
        Pot.AllBubbles.ForEach(x => x.OtherBubblePopped(this));
        var particles = Instantiate(PopParticlePrefab);
        particles.transform.position = transform.position;
        Destroy(particles, 1f);
        AudioSource.PlayClipAtPoint(PopSound, transform.position, PopSoundVolume);
    }

    public float PopPushStrength = 1f;

    public void OtherBubblePopped(Bubble bubble)
    {
        var distance = Mathf.Clamp(Vector2.Distance(bubble.transform.position, transform.position) * 0.3f, 0f, Mathf.PI * 0.5f);
        var pushStrength = Mathf.Cos(distance);
        var direction = (transform.position - bubble.transform.position).normalized ;
        _popVelocity.x += direction.x * pushStrength * PopPushStrength;
        _popVelocity.y += direction.y * pushStrength * PopPushStrength;
    }
}

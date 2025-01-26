using UnityEngine;

public class RandomizeAnimatorSpeed : MonoBehaviour
{
    public Animator Animator;
    public Vector2 RandomSpeed = new Vector2(0.8f, 1.2f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Animator == null)
            Animator = GetComponent<Animator>();
        Animator.speed = Random.Range(RandomSpeed.x, RandomSpeed.y);
    }
}

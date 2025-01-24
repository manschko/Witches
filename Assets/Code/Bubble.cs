using UnityEngine;
using UnityEngine.Events;

public class Bubble : MonoBehaviour
{
    public Item ContainedItem;
    public UnityEvent OnPopped = new UnityEvent();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPopped.Invoke();
            Destroy(gameObject);
        }
    }
}

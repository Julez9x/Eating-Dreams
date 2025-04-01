using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private Vector3 normalizeDirection;

    public Transform target;
    public float speed = 5f;
    void Start()
    {
        normalizeDirection = (target.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position += normalizeDirection * speed * Time.deltaTime;
    }
}

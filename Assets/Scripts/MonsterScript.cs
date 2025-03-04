using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private Vector3 normalizeDirection;

    public Transform target;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        normalizeDirection = (target.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normalizeDirection * speed * Time.deltaTime;
    }
}

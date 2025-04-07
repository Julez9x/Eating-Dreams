using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private Vector3 normalizeDirection = Vector3.zero;

    public Transform target;
    public float speed = 5f;
    void Start()
    {
        normalizeDirection.x = Mathf.Clamp01(target.position.x - transform.position.x);
    }

    void Update()
    {
        transform.position += normalizeDirection * speed * Time.deltaTime;
    }
}

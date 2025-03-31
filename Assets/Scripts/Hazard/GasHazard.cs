using UnityEngine;

public class GasHazard : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float gasMoveSpeed = 3f;

    private Vector3 nextPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextPos = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, gasMoveSpeed * Time.deltaTime);

        if (transform.position == nextPos)
        {
            nextPos = (nextPos == pointA.position) ? pointB.position : pointA.position;
        }
    }
}
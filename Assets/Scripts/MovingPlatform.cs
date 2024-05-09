using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public float moveSpeed = 3f;
    private Transform currentWaypoint;

    void Start()
    {
        currentWaypoint = waypoint1;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        if (transform.position == currentWaypoint.position)
        {
            if (currentWaypoint == waypoint1)
                currentWaypoint = waypoint2;
            else
                currentWaypoint = waypoint1;
        }
    }
}

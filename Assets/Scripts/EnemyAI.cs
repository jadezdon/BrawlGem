using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float enemySpeed;

    // calculated path
    public Path path;
    [HideInInspector] public bool pathIsEnded = false;

    // the max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistancce = 3f;
    // update path per second
    public float updateRate = 2f;

    Seeker seeker;
    Rigidbody2D rb;
    int currentWaypoint = 0;

    void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null) target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        // start a new path to the target position, return the result the OnPathComplete method
        seeker.StartPath(rb.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);

        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path _path)
    {
        if (!_path.error)
        {
            path = _path;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        // move enemy
        Vector2 dir = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * enemySpeed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistancce) currentWaypoint++;
    }
}

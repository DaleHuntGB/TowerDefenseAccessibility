using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
    private Transform wayPointTarget;
    private int wayPointIndex = 0;
    private float speed = 20f;
    private GameManager GameManager;

    private void Start()
    {
        wayPointTarget = WaypointManager.wayPoints[0];
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        Vector3 dir = wayPointTarget.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, wayPointTarget.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wayPointIndex >= WaypointManager.wayPoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wayPointIndex++;
        wayPointTarget = WaypointManager.wayPoints[wayPointIndex];
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
        {
            GameManager.EnemyReachedEnd();
            Destroy(gameObject);
            Debug.Log("Enemy Reached End");
        }
    }
}

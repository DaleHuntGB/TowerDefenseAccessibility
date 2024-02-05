using Unity.VisualScripting;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform enemyTarget;
    public GameObject turretBullet;
    private float fireRate = 0.5f;
    public float range = 15f;
    public float turnSpeed = 10f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            enemyTarget = nearestEnemy.transform;
        }
        else
        {
            enemyTarget = null;
        }
    }

    void Update()
    {
        if (enemyTarget == null)
        {
            return;
        }
        Vector3 dir = enemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        fireRate -= Time.deltaTime;
        if (fireRate <= 0f)
        {
            Shoot();
            fireRate = 1f;
        }
    }

    void Shoot()
    {
        Instantiate(turretBullet, transform.position, transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletController : MonoBehaviour
{
    private float bulletSpeed = 50f;
    private float bulletDmg = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController EnemyController = other.GetComponent<EnemyController>();
            EnemyController.TakeDamage(bulletDmg);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        if (transform.position.z > 50 || transform.position.z < -50 || transform.position.x > 50 || transform.position.x < -50)
        {
            Destroy(gameObject);
        }
    }
}

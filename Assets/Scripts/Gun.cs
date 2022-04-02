using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;

    [Header("BulletProperties")] [SerializeField]
    private float speed;
    void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var projectile = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

            projectile.GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
        }
    }
}

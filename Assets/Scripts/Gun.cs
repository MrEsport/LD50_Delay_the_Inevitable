using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;

    [Header("GunProperties")] 
    [SerializeField] private float coolDown;
    private float actualCoolDown;
    private bool shootbool;
    
    [Header("BulletProperties")] 
    [SerializeField] private float speed;
    void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (actualCoolDown <= 0)
        {

            if (shootbool)
            {
                var projectile = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                projectile.GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;

                actualCoolDown = coolDown;
            }
        }
        else
        {
            actualCoolDown -= Time.deltaTime;
        }

        shootbool = false;
    }


    public void shoot()
    {
        shootbool = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform startPoint;
    public float tenticleToBoatTime = 5;
    private EnemyManager enemyManager;
    bool waitingToAttack;
    [SerializeField] private float timeBetweenAttacks = 3;
    [SerializeField] private Collider2D tentacleCollider;
    private float timer = 0;
    [SerializeField] ParticleSystem particles;
    [SerializeField] private SpriteRenderer sr;

    private void Update()
    {
        if (waitingToAttack)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                waitingToAttack = false;
                tentacleCollider.enabled = true;
                Entrance();
            }
        }
    }

    public void Init(Transform startTransform, Transform endTransform, EnemyManager _enemyManager) // and boat hit event
    {
        endPoint = endTransform;
        startPoint = startTransform;
        enemyManager = _enemyManager;
        Entrance();
    }

    public void WaitToAttackAgain()
    {
        tentacleCollider.enabled = false;
        waitingToAttack = true;
        timer = timeBetweenAttacks + Random.Range(-.5f, 1f);
    }

    public void Entrance()
    {
        transform.position = startPoint.position;

        if (transform.position.x > 0) sr.flipX = true;
        
        transform.DOMoveY(transform.position.y + 2, 1).SetEase(Ease.InOutBack).onComplete += StartMovement;

    }

    public void StartMovement()
    {
        transform.DOMoveX(endPoint.transform.position.x, tenticleToBoatTime).SetEase(Ease.InCubic).onComplete += BoatReached;
        StartCoroutine(forAnimation());
    }

    IEnumerator forAnimation()
    {
        yield return new WaitForSeconds(3.5f);
        sr.GetComponent<Animator>().SetTrigger("ChangeState");
    }

    public void BoatReached()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Captain.myCaptain.OnShipHit?.Invoke();
        transform.DOMoveY(transform.position.y - 10, 1f).SetEase(Ease.Linear).onComplete += WaitToAttackAgain;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            transform.DOKill();
            enemyManager.EnemyKilled();
            Debug.Log("dead");
            Destroy((collision.gameObject));
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform startPoint;
    public float tenticleSpeed = 5;

    public void Init(Transform startTransform, Transform endTransform)
    {
        endPoint = endTransform;
        startPoint = startTransform;
        Entrance();
    }
    public void Entrance()
    {
        transform.position = startPoint.position;
        transform.DOMoveY(transform.position.y + 2, 1).SetEase(Ease.InOutBack).onComplete += StartMovement;

    }

    public void StartMovement()
    {
        transform.DOMoveX(endPoint.transform.position.x, tenticleSpeed).SetEase(Ease.InCubic).onComplete += BoatReached;
    }

    public void BoatReached()
    {
        Debug.Log("attack");
        transform.DOMoveY(transform.position.y - 2, .5f).SetEase(Ease.Linear).onComplete += Entrance ;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            transform.DOKill();
            Debug.Log("dead");
            Destroy(this);
        }
    }
}

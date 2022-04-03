using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class plankBoxe : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveX(-transform.position.x, 15).SetEase(Ease.Linear).onComplete += GoBack;
    }

    public void GoBack()
    {
        transform.DOMoveX(-transform.position.x, 15).SetEase(Ease.Linear).onComplete += GoBack;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            PlayerManager.Instance.player.AddToPlankNumber(1);
            transform.DOKill();
            ResourceManager.Instance.BarrelPickedup();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}

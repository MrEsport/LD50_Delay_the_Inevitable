using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plankBoxe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            PlayerManager.Instance.player.plank++;
            Destroy((collision.gameObject));
            Destroy(this.gameObject);
        }
    }
}

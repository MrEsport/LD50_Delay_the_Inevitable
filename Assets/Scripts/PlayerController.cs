using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Essentials")] 
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    public Gun gun;
    
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float ladderSpeed;
    [SerializeField] private boatLocation location;

    [Header("Ladders")]
    [SerializeField] private LADDERENUM closeToLadderenum;
    [SerializeField] private bool isClibling;
    public GameObject lastLadder;
    public Transform target;
    public float plusoumoins;

    [Header("Bucket")]
    public bool bucketFull;
    



    public enum boatLocation
    {
        DECK,
        HOLD,
    }
    public enum LADDERENUM
    {
        NONE,
        BOTTOM,
        TOP,
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isClibling) // Not Clibling---------------------------------------------------------------
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                switch (location)
                {
                    case boatLocation.DECK:
                        gun.shoot();
                        break;
                    case boatLocation.HOLD:
                        break;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                switch (location)
                {
                    case boatLocation.DECK:
                        if (bucketFull) bucketFull = false;
                        
                        break;
                    case boatLocation.HOLD:
                        if (!bucketFull)
                        {
                            bucketFull = true;
                            Bar.Instance.BucketRemoveFlood(10f);
                        }
                        else
                        {
                            bucketFull = false;
                            Bar.Instance.BucketAddFlood(10f);
                        }
                        break;
                }
            }
        }
        else // Clibling---------------------------------------------------------------
        {
            rb.gravityScale = 0;
            bc.enabled = false;
            float step = ladderSpeed * Time.deltaTime;
            
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            if (transform.position.y >= target.position.y - plusoumoins && transform.position.y <= target.position.y + plusoumoins) 
            {
                isClibling = false;
                rb.gravityScale = 1;
                bc.enabled = true;

                if (location == boatLocation.DECK) location = boatLocation.HOLD;
                else location = boatLocation.DECK;
            }
        }

        switch (closeToLadderenum) // Near to a ladder ----------------------------------------------------------
        {
            case LADDERENUM.TOP :
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    rb.velocity = Vector2.zero;
                    transform.position = lastLadder.GetComponent<Ladder>().topPos.position;
                    target = lastLadder.GetComponent<Ladder>().bottomPos;
                    isClibling = true;

                }

                break;
            
            case LADDERENUM.BOTTOM :
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    rb.velocity = Vector2.zero;
                    transform.position = lastLadder.GetComponent<Ladder>().bottomPos.position;
                    target = lastLadder.GetComponent<Ladder>().topPos;
                    isClibling = true;
                    
                }

                break;
        }
    }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LadderUp")
        {
            closeToLadderenum = LADDERENUM.TOP;
            lastLadder = other.transform.parent.gameObject;
        }
        else if (other.tag == "LadderDown")
        {
            closeToLadderenum = LADDERENUM.BOTTOM;
            lastLadder = other.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "LadderDown" || other.tag == "LadderUp")
        {
            closeToLadderenum = LADDERENUM.NONE;
            lastLadder = null;
        }
            
    }
}

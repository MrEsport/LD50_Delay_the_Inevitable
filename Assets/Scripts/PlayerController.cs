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
    
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float ladderSpeed;

    [Header("Ladders")]
    [SerializeField] private LADDERENUM closeToLadderenum;
    [SerializeField] private bool isClibling;
    public GameObject lastLadder;
    public Transform target;



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
        if (!isClibling)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            rb.gravityScale = 0;
            bc.enabled = false;
            float step = ladderSpeed * Time.deltaTime;
            
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            if (transform.position == target.position)
            {
                isClibling = false;
                rb.gravityScale = 1;
                bc.enabled = true;
            }
        }

        switch (closeToLadderenum)
        {
            case LADDERENUM.TOP :
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    transform.position = lastLadder.GetComponent<Ladder>().topPos.position;
                    target = lastLadder.GetComponent<Ladder>().bottomPos;
                    isClibling = true;
                    
                }

                break;
            
            case LADDERENUM.BOTTOM :
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;
    private int currentWypointIndex = 0;

    private bool moving = false;
    private SpriteRenderer SpriteRenderer;
    private float dirX;
    private Animator Animator;
    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWypointIndex].transform.position, transform.position) < .1f) 
        { 
            currentWypointIndex++;
            if (currentWypointIndex >= waypoints.Length)
            {
                currentWypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWypointIndex].transform.position, Time.deltaTime * speed);
        dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0f)
        {
            moving = true;
            SpriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            moving = true;
            SpriteRenderer.flipX = true;
        }
        else
        {
            moving = false;
        }
        Animator.SetBool("Moving",moving);
    }
}

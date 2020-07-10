using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    public GameObject sprite;
    public GameObject wheel;
    public GameObject cannon;
    public float speed;
    public float startBoost;

    private const float WHEEL_SPEED = 5;
    private const float SPIN_SPEED = 3;

    private Rigidbody2D rb;
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool startLeft = false;
    private bool startRight = false;
    private bool stopLeft = false;
    private bool stopRight = false;
    private bool outtaControl = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //TODO point cannon toward mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("Fire!");
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && !leftPressed)
        {
            leftPressed = true;
            rightPressed = false;
            startLeft = true;
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && !rightPressed)
        {
            rightPressed = true;
            leftPressed = false;
            startRight = true;
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (leftPressed)
            {
                leftPressed = false;
                stopLeft = true;
            }
            if (rightPressed)
            {
                rightPressed = false;
                stopRight = true;
            }
        }

        if (outtaControl)
        {
            sprite.transform.Rotate(Vector3.forward, SPIN_SPEED);
        }
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float xVel = xInput * speed;
        wheel.transform.Rotate(Vector3.forward, -xVel * WHEEL_SPEED);
        //float yVel = Mathf.Max(rb.velocity.y + GRAVITY_ACCEL * Time.fixedDeltaTime, MAX_FALL_SPEED);
        //float yVel = rb.velocity.y;
        Vector2 velocity = new Vector2(xVel, 0);
        if (!outtaControl)
        {
            rb.AddForce(velocity, ForceMode2D.Force);
        }
        //rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        if (startLeft)
        {
            startLeft = false;
            if (!outtaControl)
                rb.AddForce(Vector2.left * startBoost, ForceMode2D.Impulse);
        }
        if (startRight)
        {
            startRight = false;
            if (!outtaControl)
                rb.AddForce(Vector2.right * startBoost, ForceMode2D.Impulse);
        }
        if (stopLeft)
        {
            stopLeft = false;
            if (!outtaControl)
                rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
        }
        if (stopRight)
        {
            stopRight = false;
            if (!outtaControl)
                rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Landmine landmine = collision.gameObject.GetComponent<Landmine>();
        if (landmine != null)
        {
            Transform basePos = landmine.basePos;
            Vector2 dir = (transform.position - basePos.position).normalized;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(dir * landmine.ExplodePower, ForceMode2D.Impulse);
            landmine.Explode();
            outtaControl = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        outtaControl = false;
        sprite.transform.rotation = Quaternion.identity;
    }
}

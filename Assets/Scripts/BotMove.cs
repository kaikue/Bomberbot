using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    public GameObject sprite;
    public GameObject wheel;
    public GameObject cannon;
    public GameObject bombPrefab;
    public CameraShake cameraShake;
    public float speed;
    public float startBoost;
    public float bombSpeed;
    public AudioClip bonkSound;
    public AudioClip shootSound;

    private const float WHEEL_SPEED = 5;
    private const float SPIN_SPEED = 3;
    private const float SPIN_FIX_TIME = 1;

    private Rigidbody2D rb;
    private AudioSource audioSrc;
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool startLeft = false;
    private bool startRight = false;
    private bool stopLeft = false;
    private bool stopRight = false;
    private bool outtaControl = false;
    private bool stoppingSpin = false;
    private Coroutine crtStopSpin;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cannonPos = new Vector2(cannon.transform.position.x, cannon.transform.position.y);
        Vector2 bombDir = (mousePos - cannonPos).normalized;
        cannon.transform.rotation = Quaternion.FromToRotation(Vector3.right, bombDir);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bomb = Instantiate(bombPrefab, cannon.transform.position, Quaternion.identity);
            Rigidbody2D bombRB = bomb.GetComponent<Rigidbody2D>();
            bombRB.AddForce(bombDir * bombSpeed, ForceMode2D.Impulse);
            audioSrc.PlayOneShot(shootSound);
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
        Vector2 velocity = new Vector2(xVel, 0);
        if (!outtaControl)
        {
            rb.AddForce(velocity, ForceMode2D.Force);
        }

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
            Vector2 dir = (transform.position - landmine.transform.position).normalized;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(dir * landmine.ExplodePower, ForceMode2D.Impulse);
            landmine.Explode();
            outtaControl = true;
            cameraShake.Shake();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSrc.PlayOneShot(bonkSound);
        RegainControl();
    }

    private void RegainControl()
	{
        outtaControl = false;
        sprite.transform.rotation = Quaternion.identity;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (outtaControl && !stoppingSpin)
		{
            stoppingSpin = true;
            crtStopSpin = StartCoroutine(StopSpin());
		}
	}

    private IEnumerator StopSpin()
	{
        yield return new WaitForSeconds(SPIN_FIX_TIME);
        stoppingSpin = false;
        RegainControl();
    }

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (stoppingSpin)
		{
            StopCoroutine(crtStopSpin);
            stoppingSpin = false;
		}
	}
}

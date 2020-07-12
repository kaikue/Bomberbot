using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explodePower;
    public float lifetime;

    private Collider2D explosionCollider;

    private void Start()
    {
        explosionCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(Disappear());
    }

    public void DisableCollision()
	{
        explosionCollider.enabled = false;
    }

    private IEnumerator Disappear()
	{
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
	}
}

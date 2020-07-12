using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public GameObject explosionPrefab;

    private const float CASCADE_DELAY = 0.1f;

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explosion explosion = collision.gameObject.GetComponent<Explosion>();
        if (explosion != null)
        {
            StartCoroutine(DelayExplode());
        }
    }

    private IEnumerator DelayExplode()
	{
        yield return new WaitForSeconds(CASCADE_DELAY);
        Explode();
    }
}

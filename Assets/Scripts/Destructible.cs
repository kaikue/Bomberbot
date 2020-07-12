using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	public GameObject explosionPrefab;
	public float destroyTime;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<Explosion>() != null)
		{
			StartCoroutine(Break());
		}
	}

	private IEnumerator Break()
	{
		yield return new WaitForSeconds(destroyTime);
		if (explosionPrefab != null)
		{
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}
}

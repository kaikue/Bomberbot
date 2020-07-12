using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
	{
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
	}
}

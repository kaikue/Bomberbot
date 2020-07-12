using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private const int SHAKES = 10;
	private const float SHAKE_DISTANCE = 0.2f;
	private const float SHAKE_TIME = 0.02f;

    public void Shake()
	{
		StartCoroutine(CrtShake());
	}

	private IEnumerator CrtShake()
	{
		for (int i = 0; i < SHAKES; i++)
		{
			transform.localPosition = new Vector3(Random.Range(-SHAKE_DISTANCE, SHAKE_DISTANCE), Random.Range(-SHAKE_DISTANCE, SHAKE_DISTANCE), 0);
			yield return new WaitForSeconds(SHAKE_TIME);
		}
		transform.localPosition = Vector3.zero;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
	public GameObject otherBot;
	public AudioSource musicSrc;
	public float musicFadeTime;

	public void End()
	{
		GameObject.FindGameObjectWithTag("timer").GetComponent<Timer>().Stop();
		otherBot.GetComponent<Otherbot>().Activate();
		StartCoroutine(StopMusic());
	}

	private IEnumerator StopMusic()
	{
		float baseVolume = musicSrc.volume;
		for (float t = 0; t < musicFadeTime; t += Time.deltaTime)
		{
			musicSrc.volume = Mathf.Lerp(baseVolume, 0, t / musicFadeTime);
			yield return new WaitForEndOfFrame();
		}
		musicSrc.volume = 0;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Otherbot : MonoBehaviour
{
	public AudioClip fallSound;
	public AudioClip hitSound;
	public AudioClip wakeSound;
	public AudioClip loveSound;
	public Sprite wakeSprite;
	public GameObject heart;
	public float wakeTime;
	public float heartTime;
	public float endTime;

	private AudioSource audioSrc;
	private SpriteRenderer sr;

	private void Start()
	{
		audioSrc = gameObject.GetComponent<AudioSource>();
		sr = gameObject.GetComponent<SpriteRenderer>();
	}

	public void Activate()
	{
		gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		audioSrc.PlayOneShot(fallSound);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		audioSrc.PlayOneShot(hitSound);
		StartCoroutine(Wake());
	}

	private IEnumerator Wake()
	{
		yield return new WaitForSeconds(wakeTime);
		audioSrc.PlayOneShot(wakeSound);
		sr.sprite = wakeSprite;
		yield return new WaitForSeconds(heartTime);
		audioSrc.PlayOneShot(loveSound);
		heart.SetActive(true);
		yield return new WaitForSeconds(endTime);
		SceneManager.LoadScene(2);
	}
}

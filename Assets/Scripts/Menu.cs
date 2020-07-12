using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public TextMeshProUGUI timer;

	private void Start()
	{
		if (timer != null)
		{
			Timer gameTimer = GameObject.FindGameObjectWithTag("timer").GetComponent<Timer>();
			gameTimer.showTimer = false;
			string time = gameTimer.GetTimeAndReset();
			timer.text = "Time: " + time;
		}
	}

	public void LoadStart()
	{
		SceneManager.LoadScene(0);
	}

	public void LoadGame()
	{
		SceneManager.LoadScene(1);
	}

	public void Quit()
	{
		Application.Quit();
	}
}

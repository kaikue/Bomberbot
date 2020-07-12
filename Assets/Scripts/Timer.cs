using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time;
    private bool running;
    public bool showTimer;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("timer");
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	public void StartTimer()
	{
        running = true;
	}

	private void Update()
	{
        if (running)
        {
            time += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.T))
		{
            showTimer = !showTimer;
		}
	}

    public void Stop()
	{
        running = false;
	}

	public string GetTimeAndReset()
	{
        string t = GetFormattedTime();
        time = 0;
        return t;
	}

    private string GetFormattedTime()
	{
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}.{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    private void OnGUI()
    {
        if (showTimer)
        {
            GUI.Label(new Rect(0, 0, 100, 100), "Time: " + GetFormattedTime());
        }
    }
}

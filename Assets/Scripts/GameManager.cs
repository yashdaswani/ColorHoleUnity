using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isMoving = false;
    public static bool isGameOver = false;
	public static int score = 0; 


	[SerializeField] SandClock clock;

	void Start()
	{
		clock.onRoundStart += OnRoundStart;
		clock.onRoundEnd += OnRoundEnd;
		clock.onAllRoundsCompleted += OnAllRoundsCompleted;

		clock.Begin();
	}

	void OnRoundStart(int round)
	{
		Debug.Log("Round Start " + round);
	}

	void OnRoundEnd(int round)
	{
		Debug.Log("Round End " + round);
	}

	void OnAllRoundsCompleted()
	{
		Debug.Log("......Time is over...........");
	}

	void OnDestroy()
	{
		clock.onRoundStart -= OnRoundStart;
		clock.onRoundEnd -= OnRoundEnd;
		clock.onAllRoundsCompleted -= OnAllRoundsCompleted;
	}
}

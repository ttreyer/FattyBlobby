using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	private float scaleTimer;
	private float TimerBarWidth;
	private Image UITimerBar;
	private Image UIPlayer;
	private Image UIPlayerNext;

	public void Init(float scaleTimer, Vector3 current, Vector3 next)
	{
		this.scaleTimer = scaleTimer;
		UpdateUIPlayer(current, next);
	}

	void Start ()
	{
		scaleTimer = 0;
		UITimerBar   = GameObject.FindGameObjectWithTag("scaleTimerBar");
		TimerBarWidth = UITimerBar.preferredWidth;
		UIPlayer     = GameObject.FindGameObjectWithTag("scalePlayer");
		UIPlayerNext = GameObject.FindGameObjectWithTag("scalePlayerNext");
	}

	public void UpdateScaleTimer(float scaleTime)
	{
		UITimerBar.rectTransform.sizeDelta = new Vector2(TimerBarWidth * ((scaleTimer - scaleTime) * 100 / scaleTimer), UITimerBar.preferredHeight);
	}

	void UpdateUIPlayer(Vector3 current, Vector3 next)
	{
		UIPlayer.rectTransform.sizeDelta = current;
		UIPlayerNext.rectTransform.sizeDelta = next;
	}

	public void UpdatePlayerSize(Vector3 current, Vector3 next)
	{
		UpdateUIPlayer(current, next);
	}
}

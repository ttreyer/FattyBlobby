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
	private RectTransform UITimerBar;
	private RectTransform UIPlayer;
	private RectTransform UIPlayerNext;

	public void Init(float scaleTimer, Vector3 current, Vector3 next)
	{
		this.scaleTimer = scaleTimer;
		UpdateUIPlayer(current, next);
	}

	void Start ()
	{
		scaleTimer = 0;
		UITimerBar   = GameObject.FindGameObjectWithTag("scaleTimerBar").GetComponent<RectTransform>();
		TimerBarWidth = UITimerBar.rect.width;
		UIPlayer     = GameObject.FindGameObjectWithTag("scalePlayer").GetComponent<RectTransform>();
		UIPlayerNext = GameObject.FindGameObjectWithTag("scalePlayerNext").GetComponent<RectTransform>();
	}

	public void UpdateScaleTimer(float scaleTime)
	{
		UITimerBar.sizeDelta = new Vector2(TimerBarWidth * ((scaleTimer - scaleTime) * 100 / scaleTimer), UITimerBar.rect.height);
	}

	void UpdateUIPlayer(Vector3 current, Vector3 next)
	{
		UIPlayer.sizeDelta = current;
		UIPlayerNext.sizeDelta = next;
	}

	public void UpdatePlayerSize(Vector3 current, Vector3 next)
	{
		UpdateUIPlayer(current, next);
	}
}

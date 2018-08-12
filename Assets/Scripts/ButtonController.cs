using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnTriggerEvent : UnityEvent { };

public class ButtonController : MonoBehaviour
{
	public bool FlipingButton = false;
	public OnTriggerEvent OnTriggerEvent;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			OnTriggerEvent.Invoke();

			if (!FlipingButton)
				GetComponent<Collider2D>().enabled = false;
		}
	}
}

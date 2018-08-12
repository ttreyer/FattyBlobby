using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public enum PickUpEvent{Grow, Shrink};

public class PickUpController : MonoBehaviour
{    
	public PickUpEvent pickUpEvent;
	public bool OneUse;
	private PlayerController player;
	private SpriteRenderer sprite;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	private void Update()
	{
		sprite.transform.Rotate(new Vector3(0,0,45) * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Tigger");
		if (other.CompareTag("Player"))
		{
			Debug.Log("IsPlayer");
			switch (pickUpEvent)
			{
				case PickUpEvent.Grow :
					player.Grow();
					break;
				
				case PickUpEvent.Shrink :
					player.Shrink();
					break;
			}

			if (OneUse)
			{
				this.gameObject.SetActive(false);
			}
		}
	}
}

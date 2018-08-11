using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float scaleTime;
    public Vector3 initialPlayerScale;
    private float scaleTimer;
    private GameObject player;
    private PlayerController playerController;
    private GameObject ui;
    private UIController uiController;


    private void Start () {

        initScaleTimer();
        //Get player and  controller
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        //Get UI and controller
        ui = GameObject.FindGameObjectWithTag("Ui");
        uiController = ui.GetComponent<UIController>();

        uiController.Init(scaleTime);
        playerController.Init(initialPlayerScale);
    }

    private void LateUpdate () {
        scaleTimer -= Time.deltaTime;
        uiController.UpdateScaleTimer(scaleTimer);

        if (scaleTimer <= 0)
        {
            playerController.Grow();
            initScaleTimer();
        }
	}

    private void initScaleTimer()
    {
        scaleTimer = scaleTime;
    }
}

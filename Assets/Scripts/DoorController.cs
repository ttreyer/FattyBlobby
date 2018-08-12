using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {


    public bool isClose = true;


    private void Start()
    {
        SetOpen(isClose);
    }


    public void SetOpen(bool newState)
    {
        this.gameObject.SetActive(newState);
    }

    public void Toggle()
    {
        isClose = !isClose;
        SetOpen(isClose);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public enum LazerOrientation { Horizontal, Vertical }

public class LazerController : MonoBehaviour {
    public LazerOrientation orientation;
    public bool needTrigger;
    private bool state = false;

    private void Start()
    {
        if (needTrigger)
        {
            gameObject.SetActive(state);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!collision.CompareTag("Player"))
            return;

        GameObject player = collision.gameObject;
        PlayerController pc = player.GetComponent<PlayerController>();

        Vector3 delta = transform.position - player.transform.position;
        float dx = Mathf.Round(Mathf.Abs(delta.x));
        float dy = Mathf.Round(Mathf.Abs(delta.y));

        float newX = player.transform.localScale.x, newY = player.transform.localScale.y;
        if (orientation == LazerOrientation.Horizontal)
            newX = (dx > 0.0f ? dx : player.transform.localScale.x);
        else
            newY = (dy > 0.0f ? dy : player.transform.localScale.y);

        Debug.Log("New scale: " + newX + ", " + newY);

        if (dx > 0.0f || dy > 0.0f)
            pc.Cut(new Vector3(newX, newY, 1.0f));
    }

    public void Trigger()
    {
        if (needTrigger)
            gameObject.SetActive(state = !state);
        else
            Debug.LogError("Can't Trigger a Lazer without this option !");
    }
}

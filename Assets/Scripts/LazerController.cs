using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public enum LazerOrientation { Horizontal, Vertical }

public class LazerController : MonoBehaviour {
    public LazerOrientation orientation;
    public bool startingState;
    private bool state = false;
    private Collider2D trigger;

    private void Start() {
        trigger = GetComponent<Collider2D>();
        SetEnabled(startingState);
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

        if (dx > 0.0f || dy > 0.0f)
            pc.Cut(new Vector3(newX, newY, 1.0f));
    }

    public void SetEnabled(bool newState) {
        state = newState;

        trigger.enabled = state;
        foreach (Transform child in transform) {
            Animator childAnim = child.GetComponent<Animator>();
            if (childAnim == null) continue;

            childAnim.SetBool("Enabled", state);
        }
    }

    public void Toggle() {
        SetEnabled(!state);
    }
}

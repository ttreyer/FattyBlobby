using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyCollision : MonoBehaviour {
    public CollisionChecker collisionChecker;

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(name);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(name);
        collisionChecker.SetCollision(name, true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        collisionChecker.SetCollision(name, false);
    }
}

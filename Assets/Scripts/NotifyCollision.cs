using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyCollision : MonoBehaviour {
    public CollisionChecker collisionChecker;

    private void OnTriggerEnter2D(Collider2D collision) {
        collisionChecker.SetCollision(name, true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        collisionChecker.SetCollision(name, false);
    }
}

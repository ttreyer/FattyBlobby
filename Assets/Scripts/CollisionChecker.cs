using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {
    private Dictionary<string, bool> collisions = new Dictionary<string, bool>();

    public void SetCollision(string name, bool isAWall) {
        collisions[name] = isAWall;
    }

    public bool CanGo(string name) {
        bool isAWall = false;
        return !(collisions.TryGetValue(name, out isAWall) ? isAWall : false);
    }
}

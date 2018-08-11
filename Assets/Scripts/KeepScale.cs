using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScale : MonoBehaviour {
    public float scaleX = 0.0f;
    public float scaleY = 0.0f;
	
	void LateUpdate () {
        float localX = transform.localScale.x;
        float localY = transform.localScale.y;

        float globalX = transform.lossyScale.x;
        float globalY = transform.lossyScale.y;

        float newScaleX = localX * (scaleX != 0.0f ? scaleX / globalX : 1.0f);
        float newScaleY = localY * (scaleY != 0.0f ? scaleY / globalY : 1.0f);

        transform.localScale = new Vector3(newScaleX, newScaleY, 0.0f);
	}
}

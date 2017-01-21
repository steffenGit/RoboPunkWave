using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour {

    public float duration = 1.0F;
    private Light lighter;
    
    void Start () {
        lighter = GetComponent<Light>();
	}
	
	void Update () {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lighter.color = Color.Lerp(new Color(0.3f, 0, 0.3f), new Color(0.9f, 0, 0.9f), t);
    }
}

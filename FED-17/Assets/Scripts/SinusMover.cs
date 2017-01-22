using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusMover : MonoBehaviour
{
	public float speed = 1f;
	public float magnitude = 1f;
	
	// Update is called once per frame
	void Update ()
	{
		transform.localPosition = new Vector2(transform.localPosition.x, magnitude * Mathf.Sin(Time.time * speed) );
    }
}

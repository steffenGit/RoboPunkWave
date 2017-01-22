using SpriterDotNetUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriterGetAnim : MonoBehaviour
{
	public UnityAnimator animator;
	// Use this for initialization
	void Start ()
	{
		UnityAnimator animator = gameObject.GetComponent<SpriterDotNetBehaviour>().Animator;
		if( animator == null )
		{
			Debug.LogError("Could not find UnityAnimator");
		}
	}
}
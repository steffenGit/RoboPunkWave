using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
	public int ownPlayerId;
	public enum eDamageTrigger { Punch, Kick, UpperCut, CircleKick, LowerKick };

	public eDamageTrigger triggerType;
	PlayControllerScript controller_;

	void OnTriggerEnter2D( Collider2D other )
	{
		if( !controller_ )
		{
			Debug.LogError("Error: PlayControllerScript not set!");
			return;
		}

		if( !(other.tag == "Player") )
			return;

        int collPlayerId = other.GetComponent<PlayControllerScript>().GetPlayerId();
		if( collPlayerId != ownPlayerId )
		{
			controller_.DamageTriggerCallback( (int)triggerType, other );
		}
    }
	
	public void SetCallback( PlayControllerScript controller )
	{
		controller_ = controller;
		ownPlayerId = controller.GetComponent<PlayControllerScript>().GetPlayerId();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDeath : MonoBehaviour {

    [SerializeField]
    private Collider collider;

    private Character[] chars;

    public ColliderDeath(Collider collider, Character character01, Character character02, Character character03, Character character04)
    {
        if(character04 != null)
        {
            chars = new Character[4];
            chars[0] = character01;
            chars[1] = character02;
            chars[2] = character03;
            chars[3] = character04;
        }
        else if(character03 != null)
        {
            chars = new Character[3];
            chars[0] = character01;
            chars[1] = character02;
            chars[2] = character03;
        }
        else
        {
            chars = new Character[4];
            chars[0] = character01;
            chars[1] = character02;
        }
    }

    public ColliderDeath(Collider collider, Character character01, Character character02, Character character03) : this(collider, character01, character02, character03, null)
    {
    }

    public ColliderDeath(Collider collider, Character character01, Character character02) : this(collider, character01, character02, null, null)
    {
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        switch(other.name)
        {
            case "Player01":    //TODO names of the colliders should be the same
                chars[0].collidedWithElementOfDeath();
                break;

            case "Player02":
                chars[1].collidedWithElementOfDeath();
                break;

            case "Player03":
                chars[2].collidedWithElementOfDeath();
                break;

            case "Player04":
                chars[3].collidedWithElementOfDeath();
                break;
        }
    }
}

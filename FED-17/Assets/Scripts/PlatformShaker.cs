using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShaker : MonoBehaviour
{
    List<Collision> objectsOnCollider;

    Rigidbody rb;
    Transform tf;

    public float duration = 0.0f;
    public float speed = 0.5f;
    public float switchDirectionAfterTime = 50.0f;
    public float range = 5;
    
    void Start()
    {
        objectsOnCollider = new List<Collision>();
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (duration > 0.0f)
        {
            Vector3 currPos = tf.position;
            float t = Mathf.PingPong(Time.time * switchDirectionAfterTime, range) / range;
            transform.Translate(new Vector3(0, (0.5f - t) * speed, 0));
            duration -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Save the object that entered the platform
        objectsOnCollider.Add(collision);
        duration = 0.6f;
        
        /**
         * 
         * TODO trigger the code below when the player that enters collision attacks with a SHOCKWAVE
         * 
         */

        // Apply damage to all other players that are currently on the platform
        if(collision.gameObject.GetComponent<PlayControllerScript>() != null)
        {
            int idOfAttackingPlayer = collision.gameObject.GetComponent<PlayControllerScript>().playerId;
            foreach (Collision coll in objectsOnCollider)
            {
                int idOfOther = coll.gameObject.GetComponent<PlayControllerScript>().playerId;
                if (idOfOther != idOfAttackingPlayer)
                {
                    coll.gameObject.GetComponent<PlayControllerScript>().DamageTriggerCallback( // Apply damage to the player that was hit
                        ATTACKS.SHOCK_WAVE_DMG,                                                 // The attack type
                        collision.gameObject.GetComponent<Collider2D>());                       // The player that made the attack
                }
            }
        }
    }

    void onCollisionExit(Collision collision)
    {
        // Remove the object that left the platform
        objectsOnCollider.Remove(collision);
    }
}
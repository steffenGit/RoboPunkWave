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

    // Use this for initialization
    void Start()
    {
        objectsOnCollider = new List<Collision>();
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            duration = 0.5f;
        }*/

        if (duration > 0.0f)
        {
            Vector3 currPos = tf.position;
            float t = Mathf.PingPong(Time.time * switchDirectionAfterTime, range) / range;
            transform.Translate(new Vector3(0, (0.5f - t) * speed, 0));
            duration -= 0.01f;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        objectsOnCollider.Add(collision);
        duration = 0.3f;

        foreach (Collision coll in objectsOnCollider)
        {
            if (coll.gameObject.tag == "OtherPlayer") // TODO set other player to the real tag
            {
                // TODO apply damage to the other player
            }
        }
    }
}
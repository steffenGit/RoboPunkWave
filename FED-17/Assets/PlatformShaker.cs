using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShaker : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    public float speed = 0.0f;
    public float switchDirectionAfterTime = 50.0f;
    public float range = 5;

    // Use this for initialization
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            speed = 0.5f;
        }

        if (speed > 0.0f)
        {
            Vector3 currPos = tf.position;
            float t = Mathf.PingPong(Time.time * switchDirectionAfterTime, range) / range;
            Debug.Log(t);
            transform.Translate(new Vector3(0, (0.5f - t) * speed, 0));
            speed -= 0.01f;
        }

    }

    /*void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        shaking = 4.0f;
    }*/
}
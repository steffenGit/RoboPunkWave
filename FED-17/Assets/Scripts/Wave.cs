﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    List<Collision> objectsOnCollider;

    Vector3[] originalVertices;

    Vector3 waveOrigin = new Vector3(0, 0, 0);
    float timestampLastJump = 0.0f;

    float scale = 0.0f;
    float speed = 0.0f;
    float distanceFactor = 0.0f;
    float noiseStrength = 1f;
    float noiseWalk = 1f;

    int calledCounter = 0;

    private Mesh mesh;
    private Vector3[] baseHeight;
    
    void Start () {
        objectsOnCollider = new List<Collision>();

        mesh = GetComponent<MeshFilter>().mesh;
        if (baseHeight == null)
        {
            baseHeight = mesh.vertices;
        }
        // get original position of all vertices so we can reset it
        originalVertices = new Vector3[mesh.vertices.Length];
        System.Array.Copy(mesh.vertices, originalVertices, mesh.vertices.Length);
    }
	
	void Update () {
        this.drawWaves(waveOrigin);

        DecreaseFactorsOverTime();
        UpdateMash();
    }

    void OnCollisionEnter(Collision collision)
    {
        objectsOnCollider.Add(collision);
        if (collision.gameObject.tag == "Player")
        {
            PlayControllerScript playerController = collision.gameObject.GetComponent<PlayControllerScript>();
            Transform playerTransform = collision.gameObject.GetComponent<Transform>();
            Rigidbody playerRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            //if(playerController.hasJumped)
            {
                waveOrigin = playerTransform.position;
                scale = 0.1f;
                speed = 15.0f;
                distanceFactor = 1.0f;
                timestampLastJump = Time.time;
            //    playerController.hasJumped = false;
            }

            // apply damage event to other objects
            foreach(Collision coll in objectsOnCollider)
            {
                if(coll.gameObject.tag == "Enemy")
                {
                    Color color = new Color(255, 60, 80);
                    coll.gameObject.GetComponent<Renderer>().material.color = color;
                }
            }
        }
    }

    void onCollisionExit(Collision collision)
    {
        objectsOnCollider.Remove(collision);
    }

    private void drawWaves(Vector3 position)
    {
        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * scale;
            //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertex.y += GetDistanceFactor(waveOrigin, mesh.vertices[i]);
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
    }

    private float GetDistanceFactor(Vector3 playerPosition, Vector3 vertex)
    {
        float distanceToOrigin = (float)Mathf.Abs(Vector3.Distance(playerPosition, vertex));
        float scaleFactor = (float)(3 - distanceToOrigin);
        if (scaleFactor > 3)
            scaleFactor = 3;
        if (scaleFactor < 0)
            scaleFactor = 0;
        return scaleFactor * distanceFactor * -1;
    }

    private void DecreaseFactorsOverTime()
    {
        // decrease wave speed over time
        if (speed > 0.0f)
            speed -= 0.01f;
        // decrease scale over time
        if (scale > 0.0f)
            scale -= 0.01f;
        // decrease distanceFactor over time
        if (distanceFactor > 0.0f)
            distanceFactor -= 0.2f;
    }

    private void UpdateMash()
    {
        mesh.RecalculateNormals();
        //GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}

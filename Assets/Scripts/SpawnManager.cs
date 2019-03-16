﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class SpawnManager : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private GameObject upObstacle1;
    [SerializeField] private GameObject upObstacle2;
    [SerializeField] private GameObject downObstacle;

    [SerializeField] private float   spawnInterval  = 2f;
    [SerializeField] private Vector3 spawnPosDownOb = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 spawnPosUpOb   = new Vector3(0f, 0f, 0f);
    [SerializeField] private float   speed          = -10f;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        // trigger spawning new object, starting from 2s, with frequency of once each 2s
        InvokeRepeating("SpawnObject", 2.0f, 2.0f);
    }

    void Update() {
        float displacement = Time.deltaTime * speed;

        // store all children under Spawn Manager in an array
        Transform[] children = transform.Cast<Transform>().ToArray();

        for (int i = 0; i < children.Length; i++) {
            var child = children[i];
            // beware to add Space.World or otherwise default will be Space.Self
            // where rotation angle of the object will be stored as well
            child.transform.Translate(Vector2.right * displacement, Space.World);
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void SpawnObject() {
        // instantiate the next spawn
        GameObject newSpawn;

        // random 1/3 possibility spawning each of the 3 plausible objects
        Random random = new Random();
        int randomThreshold = random.Next(1, 4); // generate a integer number between 1, 2, 3

        if (randomThreshold == 1) {
            newSpawn = Instantiate(
                upObstacle1,
                spawnPosUpOb,
                Quaternion.identity);
            addChildToCurrentObject(newSpawn);
        } else if (randomThreshold == 2) {
            newSpawn = Instantiate(
                upObstacle2,
                spawnPosUpOb,
                Quaternion.identity);
            addChildToCurrentObject(newSpawn);
        } else if (randomThreshold == 3) {
            newSpawn = Instantiate(
                downObstacle,
                spawnPosDownOb,
                Quaternion.identity); // beware the trash spawn has rotation angle
            addChildToCurrentObject(newSpawn);
        }
    }

    void addChildToCurrentObject(GameObject item) {
        // make the current item a child of the SpawnManager
        item.transform.parent = transform;
    }
}
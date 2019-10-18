﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        respawn();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8f) {
            respawn();
        }
    }

    void respawn() {
        float ran = Random.Range(-9f, 9f);
        transform.position = new Vector3 (ran, 8f, 0);
    }
}

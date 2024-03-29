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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -7f) {
            respawn();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            Destroy(this.gameObject);
        } else if (other.tag == "Laser") {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        } else {
            Debug.Log("Unexpected Collision: " + other);
        }
    }

    void respawn() {
        float ran = Random.Range(-9f, 9f);
        transform.position = new Vector3 (ran, 7f, 0);
    }
}

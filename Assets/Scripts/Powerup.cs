using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    /* Powerup IDs
    Tripleshot = 0
    Speed = 1
    Shield = 2
    */
    [SerializeField]
    private int powerUpID;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -7f) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) {
                switch(powerUpID) {
                    case 0:
                      player.ActivateTripleShot();
                      break;
                    case 1:
                        player.ActivateSpeed();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default: 
                        Debug.Log("Unknown Powerup!");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}

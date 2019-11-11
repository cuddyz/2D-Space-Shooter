using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        float ran = Random.Range(-9f, 9f);
        transform.position = new Vector3(ran,7f,0);
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
                player.ActivateTripleShot();
            }
            Destroy(this.gameObject);
        }
    }
}

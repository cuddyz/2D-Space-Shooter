using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // public can be changed by other objects
    // data type (int, float, bool, string)
    // every var has a name

    // SerializeField lets it be changed in inspector, but not by other game objects
    [SerializeField]
    private float _speed = 3.5f;
    public GameObject laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // take the current position and set starting position (0,0,0)
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void FireLaser() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        }
    }

    void CalculateMovement() {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(xInput, yInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        CheckBounds();
    }

    void CheckBounds() {
        // Lock in-between y -3.8f and 0
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        // No clamp here, since we are "teleporting"
        if (transform.position.x > 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if (transform.position.x < -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
}

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
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private int _lives = 3;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedActive = false;
    
    public GameObject laserPrefab;
    public GameObject tripleShotPrefab;



    // Start is called before the first frame update
    void Start()
    {
        // take the current position and set starting position (0,0,0)
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null) {
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void FireLaser() {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire) {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive) {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            } else {
                Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            }
        }
    }

    void CalculateMovement() {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(xInput, yInput, 0);

        float currentSpeed = _speed;
        if (_isSpeedActive) {
            currentSpeed = _speed * _speedMultiplier;
        }

        transform.Translate(direction * currentSpeed * Time.deltaTime);

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

    public void Damage() {
        _lives -= 1;

        if (_lives < 1) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot() {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void ActivateSpeed() {
        _isSpeedActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine() {
        yield return new WaitForSeconds(5);
        _isSpeedActive = false;
    }
}

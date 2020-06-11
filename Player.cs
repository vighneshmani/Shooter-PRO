using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
//using System.Numerics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.50f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _Tripleshotprefab;


    private float _firerate = 0.5f;
    private float _nextfire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool isTripleShotActive = false;


    // Start is called before the first frame update
    void Start()
    {       //take the current position = new position(0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Fire();    
    }

    void CalculateMovement()
    {
        // Variable Horizontalinput to obtain the horizontal section from axes in Project Settings
        float Horizontalinput = Input.GetAxis("Horizontal");
        float Verticalinput = Input.GetAxis("Vertical");

        //Now we will move according to our choice of keys
        //transform.Translate(Vector3.right *Horizontalinput*_speed * Time.deltaTime);
        //transform.Translate(Vector3.up * Verticalinput * _speed * Time.deltaTime);
        transform.Translate(new Vector3(Horizontalinput, Verticalinput) * _speed * Time.deltaTime);

        /*if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        else if (transform.position.y <= -2.100616f)
        {
            transform.position = new Vector3(transform.position.x, -2.100616f, transform.position.z);
        }*/

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -2.100616f, 0), transform.position.z);

        if (transform.position.x > 11.35356f)
        {
            transform.position = new Vector3(-11.35356f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11.35356f)
        {
            transform.position = new Vector3(11.35356f, transform.position.y, transform.position.z);
        }
    }
    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _nextfire)
        {
            _nextfire = _nextfire + _firerate;

            if(isTripleShotActive == true)
            {
                Instantiate(_Tripleshotprefab, transform.position + new Vector3(0,0,0), Quaternion.identity);
            }

            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
            
        }
    }

    public void Damage()
    {
        _lives--;
        
        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
    
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //translate downwards at _speed
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if enemy at bottom of screen
        // enemy to respawn at top again
        if(transform.position.y < -4.09499f)
        {
            transform.position = new Vector3(Random.Range(-9.22f, 9.22f), 9.504f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            }
            //Debug.Log("Hit: " + other.transform.name);
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            //Debug.Log("Hit: " + other.transform.name);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

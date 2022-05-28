using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;
    private object other;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private Health health;
    private void Start()
    {
        health = GetComponent<Health>();
        health.OnHealthChangedEvent.AddListener(OnTakeDamage);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    private void OnTakeDamage(float health)
    {
        if (health < 0)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            
            _spawnManager.StartSpawing();
            Destroy(this.gameObject, 0.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotatet object on the zed axis!
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
}

   
  
  
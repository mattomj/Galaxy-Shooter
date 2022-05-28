using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private Health health;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        health.OnHealthChangedEvent.AddListener(OnTakeDamage);
        //null check player 
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The animator is NULL");
        }
    }

    private void OnTakeDamage(float health)
    {

        if (health < 0)
        {
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
            _player.AddScore(300);
            this.health.OnHealthChangedEvent.RemoveAllListeners();

            foreach (Transform t in this.transform)
            {
                t.gameObject.SetActive(false);
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            enemyLaser.tag = gameObject.tag;
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].tag = gameObject.tag;
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
}




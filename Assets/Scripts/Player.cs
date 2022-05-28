using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldsActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
   

    [SerializeField]
    private int _score;

    private UIManager _uiManager;
    private GameManager _gameManager; 

    [SerializeField]
    private AudioClip _laserSoundClip;
    
    private AudioSource _audioSource;

    private Health health;

    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    [SerializeField]
    private Animator _animator;

    // Start is called before the first frame update

    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.FindObjectOfType<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        health.OnHealthChangedEvent.AddListener(OnTakeDamage);

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL!");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }


        if (_gameManager._isCoopMode == false)
        {
            //current pos = new postion 
            transform.position = new Vector3(0, 0, 0);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        //if player1

        if (isPlayerOne == true)
        {
            CalculateMovement(false);
            if ((Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) && isPlayerOne == true)
            {
                FireLaser();
            }
        }


        //if player2 
        if (isPlayerTwo == true)
        {
            CalculateMovement(true);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                FireLaser();
            }
        }

    }

   


  


    private void CalculateMovement(bool isPlayer2) 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (isPlayer2)
        {
            horizontalInput = Input.GetAxis("Player2_Horizontal");
             verticalInput = Input.GetAxis("Player2_Vertical");
        }

        _animator.SetFloat("MoveHorizontal", horizontalInput);
        ///new Vector3(1, 0, 0)* 0 * 3.5f * real time

        transform.Translate(_speed * horizontalInput * Time.deltaTime * Vector3.right);

        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
          _canFire = Time.time + _fireRate;
        GameObject laser = null;
        if (_isTripleShotActive == true)
        {
          laser = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else 
        {
          laser = Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.4f, 0), Quaternion.identity);
        }
        laser.tag = gameObject.tag;
        _audioSource.Play();
    }

    public void OnTakeDamage(float health)
    {
        //if shields is active
        //do nothing....
        //deactivate shields 
        //return;
        if (_isShieldsActive == true)
        {
            this.health.CurrentHealth = health + 1;
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }


        if (health == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (health == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives((int)health);

        if (health < 1) 
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _uiManager.CheckForBestScore();
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

   
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    } 

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    //method to add 10 to the score!
    //Communicate with the UI to update the score!

} 

   
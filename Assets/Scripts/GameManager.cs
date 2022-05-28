using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    internal bool _isCoopMode = false;
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject _coopPlayers;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    private Animator _pauseAnimator;

    private void Start()
    {
        _pauseAnimator = _pauseMenuPanel.GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    private void Update()
    {
        //if the r key was pressed 
        //restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene("Single_Player"); //Current Game Scene


            //if the escape is presse
            //quit appilcation 

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();


            }
            else
            {
                Instantiate(_coopPlayers, Vector3.zero, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);



        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu_Co-Op");
        }

        //if p key 
        //pause game
        //enable pause menu 


        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            _pauseAnimator.SetBool("isPaused", true);
            Time.timeScale = 0;
        }

    } 

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}


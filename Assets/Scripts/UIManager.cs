using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    //handle to Text 
    [SerializeField]
    private Text _scoreText, bestScoreText;
    public int score, bestScore;

    private void start()
    {
        bestScore = PlayerPrefs.GetInt("High Score", 0);
        bestScoreText.text = "Best: " + bestScore;
    }

    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;
   
   
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.FindObjectOfType<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
    }

    //CheckForBestScore
    //if current greater than best Score 
    //bsetscore = currentScore 

    public void CheckForBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("High Score", bestScore);
            bestScoreText.text = "Best: " + bestScore;
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        
       

        if (currentLives <= 0)
        {
            _LivesImg.sprite = _liveSprites[0];
            GameOverSequence();
            return;
        }
        _LivesImg.sprite = _liveSprites[currentLives];
    }

    //ResumePlay
    public void ResumePlay()
    {
        GameManager gm = GameObject.FindObjectOfType<GameManager>();
        gm.ResumeGame();
    }

    //BackTOMainMenu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu_Co-Op");
    }
    void GameOverSequence()
    {
       
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public int lives=4;
    public bool isGameOver=false;
   
    public Button restartButton;
    public Button quitButton;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
       
       
        score=0;
        lives=4;
        livesText.text="Lives: "+lives;
        scoreText.text="Score: "+score;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int scoreToAdd){
        score+=scoreToAdd;
        scoreText.text="Score: "+score;
    }
    public void UpdateLives(int livesToAdd){
        lives+=livesToAdd;
        
        livesText.text="Lives: "+lives;
    }
    public void GameOver(){
        isGameOver=true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    public void QuitGame(){
        Application.Quit();
    }

}

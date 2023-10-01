using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   public List<GameObject> targets;
    public GameObject pauseScreen;
    private bool paused;
    public float spawnRate=1.0f;
    private int score;
    private int highScore;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI livesText;
    public int lives;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public int timeLeft=60;
    private int click;
    // Start is called before the first frame update
    void Start()
    {  
        highScore=PlayerPrefs.GetInt("High Score",0);
        highScoreText.text="High Score:"+highScore;   
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
                CheckForPause();
            }
            if(Input.GetKeyDown(KeyCode.Escape)){
                 Application.Quit();
            }
        if(score>highScore){
            highScore=score;
            PlayerPrefs.SetInt("High Score",highScore);
            highScoreText.text="High Score:"+highScore;
        }
           
        }
    
    public void UpdateLives(int livesToChange){
        lives+=livesToChange;
        livesText.text="Lives:"+lives;
        if(lives<=0){
            GameOver();
        }
    }

    IEnumerator SpawnTarget(){
        while(isGameActive){
            yield return new WaitForSeconds(spawnRate);
            int index=Random.Range(0,targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd){
        score+=scoreToAdd;
        scoreText.text="Score: "+score;
    }
    public void CountDown(){
        timeLeft-=Mathf.RoundToInt(Time.deltaTime);
        timeText.text="Time:"+timeLeft+"s";
        if(timeLeft==0){
            GameOver();
        }
    }
    public void GameOver(){
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive=false;
    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty){
        isGameActive=true;
        score=0;
        highScore=PlayerPrefs.GetInt("High Score",0);
        highScoreText.text="High Score:"+highScore;
         UpdateScore(0);
         UpdateLives(3);
         spawnRate/=difficulty;
        StartCoroutine(SpawnTarget());
        titleScreen.gameObject.SetActive(false);
    }
    void CheckForPause(){
        if(!paused){
            paused=true;
            pauseScreen.SetActive(true);
            Time.timeScale=0;
        }
        else{
            paused=false;
            pauseScreen.SetActive(false);
            Time.timeScale=1;
        }
    }
}


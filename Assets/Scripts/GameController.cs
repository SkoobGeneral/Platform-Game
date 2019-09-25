using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Idle, Playing, Ended, Ready};
public class GameController : MonoBehaviour
{
	//[Range (0f, 0.20f)]
    public float health = 2;
	//public float parallaxSpeed = 0.02f;
	//public RawImage background;
	//public RawImage platform;
    public GameObject uiIdle;
    public GameObject uiHealth;
    public GameObject uiScore;
    public Text pointsText;
    public Text recordText;
	public GameState gameState = GameState.Idle;
    public GameObject player;
    public AudioClip startClip;
    public AudioClip endClip;
    //public GameObject enemyGenerator;
    //public float scaleTime = 6f;
    //public float scaleFactor = .25f;
    private int points = 0;
    private AudioSource musicPlayer;
    // Start is called before the first frame update
    void Start()
    {
    	musicPlayer = GetComponent<AudioSource>();
        recordText.text = "BEST: " + GetTopScore().ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        bool userAction = Input.GetKeyDown("up") || Input.GetKeyDown("space") || Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right");
    	if (gameState == GameState.Idle && userAction) //Begin game
    	{
    		gameState = GameState.Playing;
            //uiIdle.SetActive(false);
            uiScore.SetActive(true);
            //player.SendMessage("UpdateState", "PlayerRun");
            //player.SendMessage("DustPlay");
            //enemyGenerator.SendMessage("StartGenerator");
            musicPlayer.Stop();
            musicPlayer.clip = startClip;
            musicPlayer.Play();
            //InvokeRepeating("GameTimeScale", scaleTime, scaleTime);
    	} else if (gameState == GameState.Playing) // Game on
    	{
	    	//Parallax();
	    } else if (gameState == GameState.Ended) //End game
        {
            //enemyGenerator.SendMessage("StopGenerator", false);
            Invoke("GameReady", 5);
            //ResetTimeScale(0.5f);
            //player.SendMessage("DustStop");
        } else if (gameState == GameState.Ready) //Ready to start again
        {
            if (userAction)
            {
                RestartGame();
                //ResetTimeScale();
            }
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
    void GameReady()
    {
        gameState = GameState.Ready;
    }
    void GameEnded() {
        gameState = GameState.Ended;
        musicPlayer.Stop();
        musicPlayer.loop = false;
        musicPlayer.clip = endClip;
        musicPlayer.Play();
    }
    //void GameTimeScale()
    //{
        //Time.timeScale += scaleFactor;
    //}
    //void ResetTimeScale(float newTimeScale = 1f)
    //{
        //CancelInvoke("GameTimeScale");
        //Time.timeScale = newTimeScale;
    //}
    public void IncreasePoints()
    {
        pointsText.text = "Score: " + (++points).ToString();
        if (points >= GetTopScore())
        {
            recordText.text = "Best: " + points.ToString();
            SaveScore(points);
        }
    }
    public int GetTopScore()
    {
        return PlayerPrefs.GetInt("Top Score", 0);
    }
    public void SaveScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Top Score", currentPoints);
    }
}

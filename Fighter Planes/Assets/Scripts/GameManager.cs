using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject cloud;
    public GameObject powerup;
    public GameObject coin;

    public AudioClip powerUp;
    public AudioClip powerDown;

    public int cloudSpeed;
    private int lives = 3;

    private bool isPlayerAlive;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI livesText;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI powerupText;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemyOne", 1f, 3f);
        InvokeRepeating("CreateEnemyTwo", 5f, 5f);
        StartCoroutine(CreatePowerup());
        InvokeRepeating("CreateCoin", 1f, 6f);
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        isPlayerAlive = true;
        cloudSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Restart();   
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }

    void CreateEnemyTwo() 
    { 
    
        Instantiate(enemyTwo, new Vector3(-11.5f, Random.Range(-5.25f, 1f), 0), Quaternion.Euler(0, 0, -90));
    }
    void CreateCoin()
    {
        Instantiate(coin, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
    }

    IEnumerator CreatePowerup()
    {
        Instantiate(powerup, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(3f, 6f));
        StartCoroutine(CreatePowerup());
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }

    public void EarnScore(int newScore)
    {
        score = score + newScore;
        scoreText.text = "Score: " + score;
    }




    public void Lives()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            lives = 0;
            livesText.text = "Lives: 0";
        }

    }






    public void GameOver()
    {
        isPlayerAlive = false;
        StopAllCoroutines();
        CancelInvoke();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        cloudSpeed = 0;
    }

    void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void UpdatePowerupText(string whichPowerup)
    {
        powerupText.text = whichPowerup;
    }

    public void PlayPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUp, Camera.main.transform.position);
    }

    public void PlayPowerDown()
    {
        AudioSource.PlayClipAtPoint(powerDown, Camera.main.transform.position);
    }
}

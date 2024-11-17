using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject coin;
    public GameObject powerup;
    public GameObject cloud;

    public AudioClip powerUp;
    public AudioClip powerDown;

    public int cloudSpeed;
    private bool isPlayerAlive;
    private int score;
    private int lives;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI powerupText;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("CreateEnemy2", 2f, 4f);
        InvokeRepeating("CreateEnemy3", 1f, 2f);
        InvokeRepeating("CreateCoin", 5f, 5f);
        StartCoroutine(CreatePowerUp());
        CreateSky();
        isPlayerAlive = true;
        cloudSpeed = 1;
        score = 0;
        scoreText.text = "Score: " + score;
        lives = 3;
        livesText.text = "Lives: " + lives;        
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    void CreateEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(-9f, 9f), 8f, 0), Quaternion.identity);
    }

    void CreateEnemy2()
    {
        Instantiate(enemy2, new Vector3(-12, Random.Range(1f, 5.5f), 0), Quaternion.identity);        
    }

    void CreateEnemy3()
    {
        Instantiate(enemy3, new Vector3(Random.Range(-10f, -5f), Random.Range(4f, 15f), 0), Quaternion.identity);
    }

    void CreateCoin()
    {
        Instantiate(coin, new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 0f), 0), Quaternion.identity);
    }

    IEnumerator CreatePowerUp()
    {
        Instantiate(powerup, new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 0f), 0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range (3f, 6f));
        StartCoroutine(CreatePowerUp());
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }

    }

    public void EarnScore(int howMuch)
    {
        score = score + howMuch;
        scoreText.text = "Score: " + score;
    }
        
    public void LoseLives(int howMuch)
    {
        lives = lives - howMuch;
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        isPlayerAlive = false;
        CancelInvoke();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        cloudSpeed = 0;
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
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

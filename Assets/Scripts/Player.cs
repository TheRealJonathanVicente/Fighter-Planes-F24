using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables
    //1. access level: public or private
    //2. type: int (e.g., 2, 4, 123, 3456, etc.), float (e.g, 2.5, 3.67, etc.)
    //3. name: (1) start w/ lowercase (2) if it is multiple words, then the other words start with uppercase and written together
    //4. optional: give it an initial value

    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private int lives;
    private int shooting;
    private bool hasShield;

    public GameManager gameManager;
    
    public GameObject explosion;
    public GameObject bullet;
    public GameObject shield;
    public GameObject leftThruster;
    public GameObject rightThruster;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        lives = 3;
        shooting = 1;
        hasShield = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Shooting();
    }

    void Moving()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        //Boundary Box + Looping
        if (transform.position.x > 11.5f || transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        
        if (transform.position.y > 0.46)
        {
            transform.position = new Vector3(transform.position.x, 0.46f, 0);
        }

        if (transform.position.y <= -3.54)
        {
            transform.position = new Vector3(transform.position.x, -3.54f, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (shooting)
            {
                case 1:
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 30f));
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -30f));
                    break;
            }
        }
        
    }

    public void LoseALife()
    {
        //lives = lives - 1
        //lives -= 1
        if (hasShield == false)
        {
            gameManager.LoseLives(1);
            lives -= 1;
        }
        else if (hasShield == true)
        {
            hasShield = false;
            shield.gameObject.SetActive(false);
        }
        if (lives == 0)
        {
            gameManager.GameOver();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    } 
    

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        gameManager.PlayPowerDown();
        speed = 8f;
        leftThruster.gameObject.SetActive(false);
        rightThruster.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
    }

    IEnumerator ShootingPowerDown()
    {
        yield return new WaitForSeconds(3f);
        gameManager.PlayPowerDown();
        shooting = 1;
        gameManager.UpdatePowerupText("");
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(3f);
        gameManager.PlayPowerDown();
        hasShield = false;
        shield.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        //if (whatDidIHit.tag == "Enemy")
        //{
        //    LoseALife();          
        //}

        if (whatDidIHit.tag == "PowerUp")
        {
            gameManager.PlayPowerUp();
            int powerupType = Random.Range(1,5); //this can be 1, 2, 3, or 4
            switch(powerupType)
            {
                case 1:
                    //speed powerup
                    speed = 12f;
                    gameManager.UpdatePowerupText("Picked up Speed!");
                    leftThruster.gameObject.SetActive(true);
                    rightThruster.gameObject.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    //double shot
                    shooting = 2;
                    gameManager.UpdatePowerupText("Picked up Double Shot!");
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 3:
                    //triple shot
                    shooting = 3;
                    gameManager.UpdatePowerupText("Picked up Triple Shot!");
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 4:
                    //shield
                    gameManager.UpdatePowerupText("Picked up Shield!");
                    hasShield = true;
                    shield.gameObject.SetActive(true);
                    StartCoroutine (ShieldPowerDown());
                    break;
            }
            Destroy(whatDidIHit.gameObject);
        }
    }
}

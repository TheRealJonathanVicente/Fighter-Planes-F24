using System.Collections;
using System.Collections.Generic;
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

    public GameObject explosion;
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        lives = 3;
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
        //if SPACE key is pressed create a bullet; what is a bullet?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //create a bullet object at my position with my rotation
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void LoseALife()
    {
        //lives = lives - 1
        //lives -= 1
        lives--;
        if (lives == 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Enemy")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().LoseLives(1);            
        }
        
    }
}

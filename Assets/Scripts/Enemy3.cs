using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{

    public Vector3 initialRotation = new Vector3(0, 0, -135);
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(initialRotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 3, 0) * Time.deltaTime * 3f);

        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.x > 12f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            GameObject.Find("Player(Clone)").GetComponent<Player>().LoseALife();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (whatDidIHit.tag == "Weapon")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(5);
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}

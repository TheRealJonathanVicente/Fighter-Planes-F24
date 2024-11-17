using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    // How many points the coin is worth
    public int value = 1;
    // How long the coin stays on screen
    private float timeAlive;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 4.0f;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive -= Time.deltaTime;

        if (timeAlive <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(1);
            StartCoroutine(playSoundAndDie());
        }
    }

    IEnumerator playSoundAndDie()
    {
        // Plays SFX
        source.Play();

        // Makes coin uninteractable and invisible
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.clear;

        // Waits for 1 second then destroys the coin.
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

        // If it was not done this way then the coin SFX would start the same frame the object is
        // destroyed, including the Audio Controller playing the sound.
    }
}

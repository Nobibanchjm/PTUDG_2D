using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int point = 100;
    [SerializeField] AudioClip amxu;
    AudioSource Ads;

    bool wasCollected = false;



    private void Start()
    {
        Ads = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            Ads.PlayOneShot(amxu);
            wasCollected = true;
            FindObjectOfType<Gamesession>().addScore(point);
            StartCoroutine(destroyCoindDelay(.4f));
        }
    }


    IEnumerator destroyCoindDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }    
}

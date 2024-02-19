using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip CoinAudio;
    [SerializeField] int pointsForCoin = 100;
    bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoin);
            AudioSource.PlayClipAtPoint(CoinAudio, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy (gameObject);
        }
    }

}

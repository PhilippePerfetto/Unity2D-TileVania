using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinValue = 100;
    private GameSession gameSession;
    private bool wasCollected = false;

    void Start() {
        gameSession = FindObjectOfType<GameSession>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            // Debug.Log("OnTriggerEnter2D " + other.name + " - " + coinValue);
            gameSession.UpdateScore(coinValue);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }        
    }
}

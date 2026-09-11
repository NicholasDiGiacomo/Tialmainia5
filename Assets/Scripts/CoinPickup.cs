using UnityEngine;

public class CoinPickup : MonoBehaviour
{

     [SerializeField] AudioClip coinPickupSFX;
     [SerializeField] int pointsForCoinPickup = 100;
     bool wasColllected = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !wasColllected)
        {
            wasColllected = true;
            AudioSource.PlayClipAtPoint(coinPickupSFX, transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject, 0.25f);
             FindAnyObjectByType<GameSession>().AddToScoe(pointsForCoinPickup);
        }
    }
}

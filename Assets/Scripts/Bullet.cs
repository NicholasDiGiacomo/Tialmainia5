using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     [SerializeField] float bulletSpeed = 2f;
    Rigidbody2D myRidgedBody;
    PlaayerMovement player;
    float xSpeed;
        void Start()
    {
        myRidgedBody = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlaayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    
    void Update()
    {
        myRidgedBody.linearVelocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("enimy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.5f);
    }
}

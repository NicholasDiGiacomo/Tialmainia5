using Unity.VisualScripting;
using UnityEngine;

public class EnimyMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRidgidBody;
    BoxCollider2D enumyFeet;
    Animator myAnimator;
    void Start()
    {
        myRidgidBody = GetComponent<Rigidbody2D>();
        enumyFeet = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

        void Update()
    {
        myRidgidBody.linearVelocity = new Vector2(moveSpeed, 0f);
    }
    void OnTriggerExit2D(Collider2D other) 
       {
        moveSpeed = -moveSpeed;
        Flip();
       }

    void Flip()
    {
        transform.localScale = new Vector2(-Mathf.Sign(myRidgidBody.linearVelocity.x), 1f);
    }
}

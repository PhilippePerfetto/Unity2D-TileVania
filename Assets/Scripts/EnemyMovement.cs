using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody2D;
    private float preventBugFlip = 0f;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRigidbody2D.velocity = new Vector2(moveSpeed, 0f);

        if (preventBugFlip > 0)
            preventBugFlip-= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
    }

    void OnTriggerExit2D(Collider2D other) {
        
        // Debug.Log($"FLIP {preventBugFlip} - {Time.deltaTime} - OnTriggerExit2D between {gameObject.name} and {other.gameObject.name}");
        
        if (preventBugFlip <= 0)
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
            preventBugFlip = 1f;
        }
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
        // Debug.Log("Scale : " + transform.localScale);
    }
}

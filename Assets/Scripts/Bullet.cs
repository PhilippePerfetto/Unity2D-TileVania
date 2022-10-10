using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    [SerializeField] float bulletSpeed = 20f;
    PlayerMovementScript playerMvt;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playerMvt = FindObjectOfType<PlayerMovementScript>();

        var xSpeed = playerMvt.transform.localScale.x * bulletSpeed;
        myRigidbody2D.velocity = new Vector2(xSpeed, 0f);
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter2D between " + gameObject.name + " and " + other.gameObject.name);
        

        if (other.tag == "Enemy" && other.gameObject.activeSelf)
        {
            Debug.Log("Destroy 1 : " + other.gameObject.name);
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy" && gameObject?.activeSelf == true) 
        {
            Debug.Log("Destroy 2" + other.gameObject.name);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("OnCollisionEnter2D between " + gameObject.name + " and " + other.gameObject.name);

        if (gameObject.name.StartsWith("Bullet"))
        {
            Debug.Log("Destroy 3 : " + gameObject.name);
            Destroy(gameObject);
        }
    }
}

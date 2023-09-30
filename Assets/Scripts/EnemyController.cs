using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveForce;

    [HideInInspector] public Vector3 moveDirection;
    private Transform playerTransform;
    private Rigidbody rb;
    [SerializeField] float collidePush;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = (playerTransform.position - transform.position).normalized;
        moveDirection.y = 0;

        FallCheck();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            Debug.Log("Push applied");
            Vector3 force = moveDirection * collidePush;
            collision.rigidbody.AddForce(force,ForceMode.Impulse);
            rb.AddForce(-force,ForceMode.Impulse);
        }

    }

    private void FallCheck()
    {
        if (transform.position.y < -2)
            Destroy(gameObject);

    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * moveForce);
    }

    public void ChangeSpeed (float value)
    {
        moveForce = value;
    }

    public void TurntoCenter()
    {
        moveDirection = (-transform.position).normalized;
    }
}

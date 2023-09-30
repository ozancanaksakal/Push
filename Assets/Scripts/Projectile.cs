using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5;
    private Transform myTarget;

    Vector3 direction;
    [SerializeField] float pushForce = 10;


    private void Start()
    {
        direction = (myTarget.position - transform.position).normalized;
        transform.Rotate(90, 0, 0);
        //Rigidbody rb= GetComponent<Rigidbody>();
        //rb.AddForce(direction*speed, ForceMode.Impulse);
        Destroy(gameObject, 4);
    }
    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
        transform.Translate(speed * Time.deltaTime * direction, Space.World);
    }

    public void LockToEnemy(Transform target)
    {
        myTarget = target;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.rigidbody.AddForce(direction * pushForce, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}

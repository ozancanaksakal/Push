using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 direction;
    [SerializeField] private float pushForce;
    [SerializeField] private float speed;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        transform.LookAt(playerTransform);
        direction = (playerTransform.position-transform.position).normalized;
        direction.y = 0;
        Destroy(gameObject, 15);
    }

    private void Update()
    {
        transform.Translate(direction * speed*Time.deltaTime,Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.rigidbody.AddForce(direction * pushForce,ForceMode.Impulse);
            Destroy(gameObject);
        }
        
    }
}

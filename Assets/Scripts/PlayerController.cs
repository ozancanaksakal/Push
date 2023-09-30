using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveForce = 40;
    [SerializeField] float pushForce = 30; //1. Guc
    [SerializeField] float rocketSpawnTime = 0.5f; //2.Guc
    // 3. guc
    [SerializeField] float riseTime = 0.5f;
    [SerializeField] float riseSpeed = 10;
    [SerializeField] float hammerForce = 200;

    public Transform focalTransform;
    public GameObject indicator;
    public GameObject projectile;
    public GameManager gameManager;

    private float verticalInput;
    private Rigidbody rb;

    // Powerup
    private bool hasPower;
    private PowerType myPower = PowerType.None;
    private int powerupCount = 0;
    public ParticleSystem[] effects;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        if (hasPower)
            indicator.SetActive(true);

        DieCheck();
    }
    private void DieCheck()
    {
        if (transform.position.y < -2)
        {
            gameManager.OpenMenu();
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(moveForce * verticalInput * focalTransform.forward);
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Powerup"))
        {
            Powerup powerup = other.GetComponent<Powerup>();
            myPower = powerup.myPower;
            Destroy(other.gameObject);
            powerupCount++;
            hasPower = true;
            StartCoroutine(PowerupCountDown(powerup.powerDuration));
            ParticleSystem effect = effects[(int)myPower];
            effect.transform.position = other.transform.position;
            effect.Play();
            
        }
        if (myPower == PowerType.Rocket)
            StartCoroutine(GenerateProjectiles());

        else if (myPower == PowerType.Hammer)
            StartCoroutine(HammerAttack());
    }
    private void OnCollisionEnter(Collision other)
    {
        if (myPower == PowerType.Push)
            PushPower(other);

    }
    // 1. Guc
    private void PushPower(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            other.rigidbody.AddForce(direction * pushForce, ForceMode.Impulse);
            Debug.Log("Force applied");
        }
    }
    
    //2.Guc
    IEnumerator GenerateProjectiles()
    {
        while (myPower == PowerType.Rocket)
        {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyList)
            {
                GameObject missile = Instantiate(projectile, transform.position, Quaternion.identity);
                missile.GetComponent<Projectile>().LockToEnemy(enemy.transform);
            }
            yield return new WaitForSeconds(rocketSpawnTime);
        }
    }
    //3. Guc
    IEnumerator HammerAttack()
    {
        while (myPower == PowerType.Hammer)
        {
            float jumpTime = Time.time + riseTime;
            float floorY = transform.position.y;

            while (Time.time < jumpTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, riseSpeed);
                yield return null;
            }

            while (transform.position.y > floorY)
            {
                rb.velocity = new Vector2(rb.velocity.x, -riseSpeed * 2);
                yield return null;
            }
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyList)
            {
                Vector3 direction = enemy.transform.position - transform.position;
                float distance = direction.magnitude;
                float actualForce = hammerForce / distance;
                enemy.GetComponent<Rigidbody>().AddForce(direction.normalized * actualForce, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator PowerupCountDown(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (powerupCount == 1)
        {
            hasPower = false;
            myPower = PowerType.None;
            indicator.SetActive(false);
            powerupCount= 0;
        }
        else powerupCount--;        
    }
}

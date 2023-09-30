using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject enemyRocket;
    [SerializeField] float innerRadius;
    [SerializeField] float outerRadius;
    [SerializeField] float highSpeed;
    [SerializeField] float centerSpeed;
    [SerializeField] float rocketTime;
    float normalSpeed;
    private EnemyController myCont;
    
    private void Start()
    {     
        myCont = GetComponent<EnemyController>();
        normalSpeed = myCont.moveForce;
        StartCoroutine(LaunchRocket());
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > outerRadius || Mathf.Abs(transform.position.z) > outerRadius)
        {
            myCont.ChangeSpeed(centerSpeed);            
        }
        else if (Mathf.Abs(transform.position.x) > innerRadius || Mathf.Abs(transform.position.z) > innerRadius)
            myCont.ChangeSpeed(highSpeed);
        
        else if (Mathf.Abs(transform.position.x) < outerRadius || Mathf.Abs(transform.position.z) < outerRadius)
            myCont.ChangeSpeed(normalSpeed);
    }

    public void LateUpdate()
    {
        if (myCont.moveForce == centerSpeed)
        {
            myCont.TurntoCenter();
        }
    }
    IEnumerator LaunchRocket()
    {
        while (true)
        {
            yield return new WaitForSeconds(rocketTime);
            Instantiate(enemyRocket, transform.position, enemyRocket.transform.rotation);
            Debug.Log("rocket");            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Vector3.zero,innerRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, outerRadius);
    }
}

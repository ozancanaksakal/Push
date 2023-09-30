using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    public Transform player;
    public float rotSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y - 0.41f, player.position.z);
        transform.Rotate(Vector3.up, rotSpeed*Time.deltaTime);
    }
}

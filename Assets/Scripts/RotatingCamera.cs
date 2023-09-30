using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{

    [SerializeField] float rotationSpeed;

    private float rotateInput;

    private void Update()
    {
        rotateInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime);
        
    }




}

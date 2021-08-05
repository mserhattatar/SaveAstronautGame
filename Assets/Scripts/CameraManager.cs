using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{ 
    public bool moveAstronaut;
    private void FixedUpdate()
    {
        if (moveAstronaut)
        {
            transform.position += Vector3.up * Time.deltaTime;
        }
    }
}

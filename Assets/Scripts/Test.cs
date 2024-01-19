using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var movementSpeed = 5f;
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}

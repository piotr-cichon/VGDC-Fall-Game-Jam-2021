using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().IncrementScore();
            Destroy(this.gameObject);
        }
    }
}

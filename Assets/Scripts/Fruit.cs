using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public enum FruitType
    {
        Banana,
        Apple,
    }
    [SerializeField] private int points;
    [SerializeField] private FruitType type;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().IncrementScore(points,type);
            Destroy(this.gameObject);
        }
    }
}

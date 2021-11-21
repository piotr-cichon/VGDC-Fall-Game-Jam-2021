using System;
using UnityEngine;

public class PortalPickup : MonoBehaviour
{
    public static Action onPortalPickup;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
            onPortalPickup?.Invoke();
            other.GetComponent<Player>().EnableTeleport();
        }
    }
}
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject doubleJumpImage;
    [SerializeField] private GameObject teleportShootImage;

    void Awake()
    {
        doubleJumpImage.SetActive(false);
        teleportShootImage.SetActive(false);
    }

    private void ActivateDoubleJump()
    {
        doubleJumpImage.SetActive(true);
        teleportShootImage.SetActive(false);
    }

    private void ActivateTeleport()
    {
        doubleJumpImage.SetActive(false);
        teleportShootImage.SetActive(true);
    }
    private void OnEnable()
    {
        DoubleJumpPowerup.onDoubleJumpPickup += ActivateDoubleJump;
        PortalPickup.onPortalPickup += ActivateTeleport;
    }
    private void OnDisable()
    {
        DoubleJumpPowerup.onDoubleJumpPickup -= ActivateDoubleJump;
        PortalPickup.onPortalPickup -= ActivateTeleport;
    }
}
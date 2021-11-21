using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private KeyCode pauseMenuKey = KeyCode.Q;
    [SerializeField] private GameObject doubleJumpImage;
    [SerializeField] private GameObject teleportShootImage;
    [SerializeField] private GameObject pauseMenu;

    private Player _player;
    void Awake()
    {
        _player = FindObjectOfType<Player>();
        pauseMenu.SetActive(false);
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

    private void Update()
    {
        if (Input.GetKeyUp(pauseMenuKey))
        {
            PauseMenu();
        }

        if (Input.GetKeyUp(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            ResumeMenu();
        }
    }

    public void ResumeMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PauseMenu()
    {
        
        pauseMenu.SetActive(true);
    }
}
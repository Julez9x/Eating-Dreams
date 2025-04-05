using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    public GameObject deathMenuUI;
    public int Respawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            DeathMenu();
        }
    }

    public void DeathMenu() 
    {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}

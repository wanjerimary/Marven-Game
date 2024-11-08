using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    GameManager Instance;
    int score = 0;
    GameObject pauseMenuUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called by the UI pause button
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // Called by the UI resume button
    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            AddScore(10);
            Destroy(other.gameObject);
        }
    }




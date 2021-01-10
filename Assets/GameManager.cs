using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject startGameCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;

    [SerializeField] private GameObject objectPool;
    [SerializeField] private GameObject roadGenerator;

    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;

    [SerializeField] private GameObject palyer;


    public GameManager Instantiate;
    
    
    private void Awake()
    {
       gameOverCanvas.SetActive(false);
       gameCanvas.SetActive(false);
       objectPool.SetActive(false);
       roadGenerator.SetActive(false);
       palyer.SetActive(false);
    }

    private void Start()
    {
        Instantiate = this;
        startButton.GetComponent<Button>().onClick.AddListener(StartGame);
        restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
    }


    private void StartGame()
    {
        palyer.SetActive(true);
        startGameCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        objectPool.SetActive(true);
        roadGenerator.SetActive(true);
    }
    
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void GameOver()
    {
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}

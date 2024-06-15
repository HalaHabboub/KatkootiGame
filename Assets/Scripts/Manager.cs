using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public int levelCount = 40;
    public int count = 0;
    public Text coin = null;
    public Text distance = null;
    public Camera cam = null;
    public GameObject guiGameOver = null;
    private bool levelOne = false;
    private bool levelTwo = false;
    private bool levelThree = false;

    public GroundsSpawner groundsSpawner = null;

    private int currentCoins = 0;
    public int currentDistance = 0;
    private bool canPlay = false;

    private static Manager s_Instance;
    public static Manager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(Manager)) as Manager;
            }

            return s_Instance;
        }
    }

    private void Start()
    {

    }

    void Update()
    {

    }

    public void UpdateCoinCount(int value)
    {
        Debug.Log("Player picked up another coin for " + value);
        currentCoins += value;
        coin.text = currentCoins.ToString();
    }

    public void UpdateDistanceCount()
    {
        if ((currentDistance == 1 && !levelOne))
        {
            levelOne = true;
            groundsSpawner.challengeGenerator();

        }
        if ((currentDistance == 10 && !levelTwo))
        {
            levelTwo = true;
            groundsSpawner.challengeGenerator();
        }
        if ((currentDistance == 20 && !levelThree))
        {
            levelThree = true;
            groundsSpawner.challengeGenerator();
        }
        Debug.Log("Player moved forward for one point");
        currentDistance += 1;
        distance.text = currentDistance.ToString();

        groundsSpawner.randomGenerator();

    }


    public bool CanPlay()
    {
        return canPlay;
    }

    public void StartPlay()
    {
        canPlay = true;
    }

    public void GameOver()
    {
        cam.GetComponent<CameraFollow>().enabled = false;
        GuiGameOver();
    }

    void GuiGameOver()
    {
        Debug.Log("Game over!");

        guiGameOver.SetActive(true);
    }

    // public void PlayAgain()
    // {
    //     Scene scene = SceneManager.GetActiveScene();
    //     SceneManager.LoadScene(scene.name);
    // }

    // public void Quit()
    // {
    //     Application.Quit();
    // }
}

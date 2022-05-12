using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region PRIVATE VARIABLES
    private int maxNumLives = 3;
    private int lives;
    private int score;
    #endregion

    #region SINGLETON

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    // Start is called before the first frame update
    #region MONOBEHAVIOUR METHODS
    void Start()
    {
        lives = maxNumLives;
        StartCoroutine(SpawnAsteroids());
    }
    #endregion

    #region PUBLIC METHODS
    // Lose a life.
    public void LoseLife()
    {
        lives--;

        if (lives == 0)
            Restart();
    }

    // Gain points.
    public void GainPoints(int points)
    {
        score += points;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    // Spawn asteroids every few seconds.
    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            // SpawnAsteroid();

            yield return new WaitForSeconds(Random.Range(2f, 8f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

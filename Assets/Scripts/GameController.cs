using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject RestartButton;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public Text scoreText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;

    private int _score;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    void Start ()
    {
        gameOver = false;
        restart = false;
        RestartButton.SetActive(false);
        gameOverText.text = "";
        _score = 0;

        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    public void RestartGame()
    {
        if (restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        
    } 

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i <= hazardCount; i++)
            {
                var hazard = hazards[Random.Range(0,hazards.Length)];
                var spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                var spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartButton.SetActive(true);
                restart = true;
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + _score;
    }

    public void AddScore(int newScoreValue)
    {
        _score += newScoreValue;
        UpdateScore();
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}

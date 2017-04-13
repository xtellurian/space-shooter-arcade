using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject PlayerExplosion;
    public int ScoreValue;
    private GameController _gameController;

    void Start()
    {
        var gameControllerObj = GameObject.FindWithTag("GameController");
        if (gameControllerObj != null)
        {
            _gameController = gameControllerObj.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cant find 'GameController' instance");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) return;

        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }
       

        if (other.tag == "Player")
        {
            Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);
            _gameController.GameOver();
        }
        
        _gameController.AddScore(ScoreValue);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}

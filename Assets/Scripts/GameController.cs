using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
     public GameObject enemyPrefab1;
    private GameManager gameManager;
    public GameObject[] PowerUp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GameControllerPrefab()
    {
        while(gameManager.gameOver == false)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-5.94f,5.88f),5.69f,0),Quaternion.identity);
            yield return new WaitForSeconds(8f);
        }
    }
    IEnumerator GameControllerPrefab1()
    {
        while(gameManager.gameOver == false)
        {
            Instantiate(enemyPrefab1, new Vector3(Random.Range(-5.94f,5.88f),5.69f,0),Quaternion.identity);
            yield return new WaitForSeconds(30f);
        }
    }
    IEnumerator GameControllerPowerUps()
    {
        while(gameManager.gameOver == false)
        {
            Instantiate(PowerUp[Random.Range(0,3)], new Vector3(Random.Range(-5.94f,5.88f),5.69f,0),Quaternion.identity);
            yield return new WaitForSeconds(8f);
        }
    }
    public void ScheduleEnemySpawner()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(GameControllerPrefab());
        StartCoroutine(GameControllerPrefab1());
        StartCoroutine(GameControllerPowerUps());
    }
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("GameControllerPrefab");
        CancelInvoke("GameControllerPrefab1");
        CancelInvoke("GameControllerPowerUps");

    }
}

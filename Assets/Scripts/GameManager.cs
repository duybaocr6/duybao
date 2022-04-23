using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public GameObject playButton;
    public GameObject backButton;
    public GameObject playerShip;
    public GameObject GameControllerGo;
    public GameObject GameOverGo;
    public GameObject scoreUITextGo;
    public enum GameManagerState {
        Opening,
        Gameplay,
        GameOver,
    }
    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
       GMState = GameManagerState.Opening;
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGo.SetActive(false);
                playButton.SetActive(true);
                backButton.SetActive(true);
            break;
            case GameManagerState.Gameplay:
                scoreUITextGo.GetComponent<GameScore>().Score=0;
                playButton.SetActive(false);
                backButton.SetActive(false);
                playerShip.GetComponent<PlayerController>().Init();
                GameControllerGo.GetComponent<GameController>().ScheduleEnemySpawner();
            break;
            case GameManagerState.GameOver:
                GameControllerGo.GetComponent<GameController>().UnscheduleEnemySpawner();
                GameOverGo.SetActive(true);
                Invoke("ChangeToOpeningState",5f);
            break;
        }
    }
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
    public void Load_Scene(string name) 
    {
        Application.LoadLevel(name);
    }
}
   

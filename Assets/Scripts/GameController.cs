using UnityEngine;
using System.Collections;

public enum GameStates
{
    START,
    WAITGAME,
    INGAME,
    GAMEOVER,
    RANKING,
}

public class GameController : MonoBehaviour
{


    public Transform player;

    private Vector3 startPositionPlayer;

    private GameStates currentState = GameStates.START;

    public TextMesh numberScore;
    public TextMesh shadowScore;

    public float timeToRestart;
    private float currentTimeToRestart;

    private int score;

    private GameOverController gameOverController;

    // Use this for initialization
    void Start()
    {
        startPositionPlayer = player.position;

        gameOverController = FindObjectOfType(typeof(GameOverController)) as GameOverController;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameStates.START:
                {
                    player.position = startPositionPlayer;
                    currentState = GameStates.WAITGAME;

                }
                break;
            case GameStates.WAITGAME:
                {
                    player.position = startPositionPlayer;


                }
                break;
            case GameStates.INGAME:
                {
                    numberScore.text = score.ToString();
                    shadowScore.text = score.ToString();
                }
                break;
            case GameStates.GAMEOVER:
                {

                    currentTimeToRestart += Time.deltaTime;

                    if (currentTimeToRestart > timeToRestart)
                    {
                        currentTimeToRestart = 0;
                        currentState = GameStates.RANKING;

                        gameOverController.SetGameOver(score);

                    }


                }
                break;
            case GameStates.RANKING:
                {


                    numberScore.GetComponent<Renderer>().enabled = false;
                    shadowScore.GetComponent<Renderer>().enabled = false;

                }
                break;
        }


    }

    public void StartGame()
    {
        currentState = GameStates.INGAME;
        numberScore.GetComponent<Renderer>().enabled = true;
        shadowScore.GetComponent<Renderer>().enabled = true;
        score = 0;
        gameOverController.HideGameOver();
    }

    public GameStates GetCurrentState()
    {
        return currentState;
    }

    public void CallGameOver()
    {
        if (currentState != GameStates.GAMEOVER)
            SoundController.PlaySound(soundsGame.hit);

        currentState = GameStates.GAMEOVER;


    }

    public void ResetGame()
    {
        player.position = startPositionPlayer;
        player.GetComponent<PlayerBehaviour>().RestartRotation();
        ObstaclesBehaviour[] pipes = FindObjectsOfType(typeof(ObstaclesBehaviour)) as ObstaclesBehaviour[];
        foreach (ObstaclesBehaviour o in pipes)
        {
            o.gameObject.SetActive(false);
        }

        numberScore.GetComponent<Renderer>().enabled = false;
        shadowScore.GetComponent<Renderer>().enabled = false;
        numberScore.text = score.ToString();
        shadowScore.text = score.ToString();
        player.position = startPositionPlayer;
    }

    public void AddScore()
    {
        score++;
        SoundController.PlaySound(soundsGame.point);
    }

}

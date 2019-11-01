using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Running,
    Pause
}
public class Pause : MonoBehaviour {
    public GameState state = GameState.Running;
    public void OnMouseUpAsButton()
    {
        ChangeGameState();
    }
    void ChangeGameState()
    {
        if (state == GameState.Running)
        {
            StopGame();
        }
        else if (state == GameState.Pause)
        {
            ContinueGame();
        }
    }
    void StopGame()//暂停游戏
    {
        Time.timeScale = 0;
        state = GameState.Pause;
    }
    void ContinueGame()//继续 
    {
        Time.timeScale = 1;
        state = GameState.Running;
    }
}
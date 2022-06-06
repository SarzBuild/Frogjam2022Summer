using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BasePlayerScene");
    }

    public void LoadEndlessRunnerScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gym_EndlessRunner");
    }

    public void LoadTicTacToeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TicTacToe");
    }

    public void LoadBulletHell()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BulletHell");
    }

}

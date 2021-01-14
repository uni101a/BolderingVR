using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart(){
        ScoreManager.DestroySingleton();
        SceneManager.LoadScene("main");
    }
}
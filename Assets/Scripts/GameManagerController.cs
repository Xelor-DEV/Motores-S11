using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameManagerController : MonoBehaviour
{
    public static GameManagerController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void LoadScene(string scene)
    {
        // Este metodo se esta utilizando para evitar que salgan errores null reference por la destruccion de todos los objetos de la escena
        DOTween.KillAll();
        SceneManager.LoadScene(scene);
    }
}

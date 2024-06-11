using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class UIManagerController : MonoBehaviour
{
    public static UIManagerController Instance { get; private set; }
    [SerializeField] private TMP_Text life;
    [SerializeField] private TMP_Text activeTorch;
    [SerializeField] private PlayerController player;
    [SerializeField] private Button pauseButton;
    [Header("Menus")]
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pause;
    [SerializeField] private RectTransform standbyPosition;
    [SerializeField] private RectTransform objective;
    [SerializeField] private float duration;
    [SerializeField] private Ease animationType;
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
    public void Pause()
    {
        PlayerController.Instance.StopMovement();
        pauseButton.interactable = false;
        pause.transform.position = objective.position;
        pause.transform.localScale = Vector3.zero;
        pause.transform.DOScale(Vector3.one, duration).SetEase(animationType).OnComplete(() =>
        {
            Time.timeScale = 0f;
        });
        
    }
    public void Continue()
    {
        PlayerController.Instance.ResumeMovement();
        Time.timeScale = 1f;
        pause.transform.DOScale(Vector3.zero, duration).OnComplete(() =>
            {
                pause.transform.position = standbyPosition.position;
                
            });
        pauseButton.interactable = true;
    }
    public void Win()
    {
        pauseButton.interactable = false;
        win.transform.DOMove(objective.position, duration).SetEase(animationType).OnComplete(() =>
        {
            Time.timeScale = 0;

        });
        PlayerController.Instance.StopMovement();

    }

    public void GameOver()
    {
        pauseButton.interactable = false;
        gameOver.transform.DOMove(objective.position, duration).SetEase(animationType).OnComplete(() =>
        {
            Time.timeScale = 0;

        });
        PlayerController.Instance.StopMovement();
    }

    private void OnEnable()
    {
        player.OnLifeChanged += UpdateLife;
    }
    private void OnDisable()
    {
        player.OnLifeChanged -= UpdateLife;
    }
    public void UpdateLife(int actualLife)
    {
        life.text = "Vida: " + actualLife;
    }
    public void UpdateActiveTorchs()
    {
        activeTorch.text = "Antorchas Activadas: " + player.TorchsActive + "/" + player.MaxTorchsActive;
    }
}

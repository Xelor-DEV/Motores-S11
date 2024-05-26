using UnityEngine;
using TMPro;
public class UIManagerController : MonoBehaviour
{
    public static UIManagerController Instance { get; private set; }
    [SerializeField] private TMP_Text life;
    [SerializeField] private TMP_Text activeTorch;
    [SerializeField] private PlayerController player;
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

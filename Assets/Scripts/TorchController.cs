using UnityEngine;
public class TorchController : MonoBehaviour
{
    [SerializeField] private TorchMaterialConfig[] materialsConfig;
    private Material actualMaterial;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject flames;
    private bool isActivated = false;
    public bool IsActivated
    {
        get
        {
            return isActivated;
        }
        set
        {
            isActivated = value;
        }
    }
    private void Start()
    {
        actualMaterial = flames.GetComponent<MeshRenderer>().materials[0];
        ChangeMaterialSettings(0);
    }
    private void OnEnable()
    {
        player.OnPlayerActiveTorch += ActivateTorch;
    }

    private void OnDisable()
    {
        player.OnPlayerActiveTorch -= ActivateTorch;
    }

    private void ChangeMaterialSettings(int index)
    {
        if (index >= 0 && index < materialsConfig.Length)
        {
            TorchMaterialConfig config = materialsConfig[index];
            actualMaterial.SetColor("_BaseColor", config.BaseColor);
            actualMaterial.SetColor("_EmissionColor", config.EmissionColor);
            actualMaterial.SetFloat("_AmbientOcclusion", config.AmbientOcclusion);
        }
    }
    public void ActivateTorch(TorchController torch)
    {
        if (torch == this && !isActivated)
        {
            isActivated = true;
            ChangeMaterialSettings(1);
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "TorchMaterialConfig", menuName = "Custom/Torch Material Config")]
public class TorchMaterialConfig : ScriptableObject
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _emissionColor;
    [SerializeField] private float _ambientOcclusion;
    public Color BaseColor
    {
        get
        {
            return _baseColor;
        }
        set
        {
            _baseColor = value;
        }
    }
    public Color EmissionColor
    {
        get
        {
            return _emissionColor;
        }
        set
        {
            _emissionColor = value;
        }
    }
    public float AmbientOcclusion
    {
        get
        {
            return _ambientOcclusion;
        }
        set
        {
            _ambientOcclusion = value;
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int life;
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
            OnLifeChanged?.Invoke(life);
            if (life <= 0)
            {
                GameManagerController.Instance.LoadScene("GameOver");
            }
        }
    }
    [SerializeField] private float speed;
    [SerializeField] private float brakeForce;
    [SerializeField] private int torchsActive;
    public int TorchsActive
    {
        get
        {
            return torchsActive;
        }
    }

    [SerializeField] private int maxTorchsActive;
    public int MaxTorchsActive
    {
        get
        {
            return maxTorchsActive;
        }
    }
    private Vector2 movement = Vector2.zero;
    private Rigidbody _compRigidbody;
    public Action<int> OnLifeChanged;
    public Action<TorchController> OnPlayerActiveTorch;
    private void Awake()
    {
        _compRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        OnLifeChanged?.Invoke(life);
        UIManagerController.Instance.UpdateActiveTorchs();
    }
    public void SetDirection(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        _compRigidbody.AddForce(new Vector3(movement.x * speed, 0 , movement.y * speed));
        if (movement.magnitude < 0.2f)
        {
            Vector3 brakeForce = -_compRigidbody.velocity.normalized * _compRigidbody.velocity.magnitude * this.brakeForce;
            _compRigidbody.AddForce(brakeForce, ForceMode.Force);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Torch")
        {
            TorchController torch = collision.gameObject.GetComponent<TorchController>();
            if (torch != null && torch.IsActivated == false)
            {
                OnPlayerActiveTorch?.Invoke(torch);
                torchsActive = torchsActive + 1;
                UIManagerController.Instance.UpdateActiveTorchs();
            }
        }
        else if (collision.gameObject.tag == "Wall")
        {
            WallController wall = collision.gameObject.GetComponent<WallController>();
            Life = Life - wall.Damage;
        }
        else if (collision.gameObject.tag == "Portal")
        {
            if (torchsActive >= maxTorchsActive)
            {
                GameManagerController.Instance.LoadScene("Win");
            }
        }
    }
}

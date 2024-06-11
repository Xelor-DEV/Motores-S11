using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [SerializeField] private int life;
    [SerializeField] private float speed;
    private Vector3 velocityBackup;
    private bool canMove = true;
    [SerializeField] private float brakeForce;
    [SerializeField] private int torchsActive;
    [SerializeField] private int maxTorchsActive;
    private Vector2 movement = Vector2.zero;
    private Rigidbody _compRigidbody;
    public Action<int> OnLifeChanged;
    public UnityEvent OnPlayerDeath;
    public UnityEvent OnPlayerWin;
    public Action<TorchController> OnPlayerActiveTorch;
    public int TorchsActive
    {
        get
        {
            return torchsActive;
        }
    }
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
                OnPlayerDeath?.Invoke();
            }
        }
    }
    public int MaxTorchsActive
    {
        get
        {
            return maxTorchsActive;
        }
    }
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
        if(canMove == true)
        {
            _compRigidbody.AddForce(new Vector3(movement.x * speed, 0, movement.y * speed));
            if (movement.magnitude < 0.2f)
            {
                Vector3 brakeForce = -_compRigidbody.velocity.normalized * _compRigidbody.velocity.magnitude * this.brakeForce;
                _compRigidbody.AddForce(brakeForce, ForceMode.Force);
            }
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
        else if (collision.gameObject.tag == "Enemy")
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            Life = Life - enemy.Damage;
        }
        else if (collision.gameObject.tag == "Portal")
        {
            if (torchsActive >= maxTorchsActive)
            {
                OnPlayerWin?.Invoke();
            }
        }
    }
    public void StopMovement()
    {
        canMove = false;
        velocityBackup = _compRigidbody.velocity;
        _compRigidbody.velocity = Vector3.zero;

    }
    public void ResumeMovement()
    {
        canMove = true;
        _compRigidbody.velocity = velocityBackup;
    }
}

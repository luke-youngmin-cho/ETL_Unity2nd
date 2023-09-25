using UnityEngine;
using FSM;
using PlayerMachine = FSM.PlayerMachine;

public class PlayerController : MonoBehaviour
{
    public float horizontal => Input.GetAxis("Horizontal");
    public float vertical => Input.GetAxis("Vertical");
    public bool isMovable { get; set; }
    public Vector3 move { get; set; }
    [SerializeField] private float _moveSpeed = 1.5f;
    private Rigidbody _rigidbody;

    public bool isGrounded { get; private set; }
    [SerializeField] private Vector3 _groundDetectCenter;
    [SerializeField] private float _groundDetectRadius;
    [SerializeField] private LayerMask _groundDetectMask;

    private PlayerMachine _machine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _machine = new PlayerMachine(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _machine.ChangeState(2);
        }

        _machine.Update();

        if (isMovable)
        {
            move = new Vector3(horizontal, 0.0f, vertical);
        }
    }

    private void FixedUpdate()
    {
        DetectGround();

        _machine.FixedUpdate();
        Move();
    }

    private void LateUpdate()
    {
        _machine.LateUpdate();
    }

    public void Move()
    {
        _rigidbody.position += move * _moveSpeed * Time.fixedDeltaTime;
    }

    private void DetectGround()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position + _groundDetectCenter,
                                                _groundDetectRadius,
                                                _groundDetectMask);
        isGrounded = cols.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + _groundDetectCenter,
                              _groundDetectRadius);
    }
}

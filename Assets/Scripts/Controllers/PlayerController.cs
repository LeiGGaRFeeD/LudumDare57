using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _swimSpeed = 3f, _waterDrag = 2f, _acceleration = 5f;
    [SerializeField] private float _rotationSpeed = 300f, _bobbingAmplitude = 0.1f, _bobbingFrequency = 1.5f;

    private Rigidbody2D _rb;
    private Vector2 _targetVelocity;
    private float _baseY, _bobbingTimer;
    private Vector3 _defaultScale;
    private GameManager _gameManager;
    
    private OxygenSystem oxygenSystem;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.drag = _waterDrag;
        _baseY = transform.localPosition.y;

        _defaultScale = transform.localScale;

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        oxygenSystem = GetComponent<OxygenSystem>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

   private void MovePlayer()
{
    Vector2 desiredVelocity = _targetVelocity;

    // Ограничение подъема выше zeroDepth
    if (transform.position.y > _gameManager.zeroDepth.position.y && desiredVelocity.y > 0f)
    {
        desiredVelocity.y = 0f;
    }

    _rb.velocity = Vector2.Lerp(_rb.velocity, desiredVelocity, _acceleration * Time.fixedDeltaTime);

    // Поворот в сторону движения
    if (_rb.velocity.sqrMagnitude > 0.01f)
    {
        float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);

        // Флип по Y
        Vector3 scale = _defaultScale;
        scale.y *= (angle > 90 || angle < -90) ? -1f : 1f;
        transform.localScale = scale;
    }

    // Покачивание
    if (_rb.velocity.sqrMagnitude < 0.05f)
    {
        _bobbingTimer += Time.fixedDeltaTime * _bobbingFrequency;
        float offsetY = Mathf.Sin(_bobbingTimer) * _bobbingAmplitude;
        Vector3 pos = transform.localPosition;
        pos.y = _baseY + offsetY;
        transform.localPosition = pos;
    }
    else
    {
        _bobbingTimer = 0f;
        _baseY = transform.localPosition.y;
    }
}


    private void GetInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        _targetVelocity = new Vector2(moveX, moveY) * _swimSpeed;
    }

    public float GetCurrentOxygen()
    {
        return oxygenSystem.GetCurrentOxygen();
    }
}

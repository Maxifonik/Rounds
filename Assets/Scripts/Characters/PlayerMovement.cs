using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    // ��������� � ������ ��������������� ��������
    // ����� ������������ � ��� ��������� ������ ����� � ����������
    private const string MovementHorizontalKey = "Horizontal";

    // ��������� ����������
    [SerializeField] private float _extraGravityMultiplier = 2f;

    // �������� ��������
    [SerializeField] private float _movementSpeed = 20f;

    // ���� ������
    [SerializeField] private float _jumpForce = 45f;

    // ������������ ������
    [SerializeField] private float _jumpDuration = 1f;

    // ��������� Rigidbody �� ���������� �������
    private Rigidbody _rigidbody;

    // ������� ������
    private Camera _mainCamera;

    // ���� ����, ��� ����� ����� �������
    private bool _canJump;

    // ���� ����, ��� ����� � ������
    private bool _isJumping;

    // ������ ������������ ������
    private float _jumpTimer;

    protected override void OnInit()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _mainCamera = Camera.main;

        if (!PhotonView || !PhotonView.IsMine)
        {
            // ���������� ��������� ������ �� ���������
            // ��� ��� ������ ����� ����� �������� �� ��� �������
            Destroy(_rigidbody);
        }
    }

    private void FixedUpdate()
    {
        if (!PhotonView || !PhotonView.IsMine || !_rigidbody)
        {
            // �����: ������� �� ������
            return;
        }

        ExtraGravity();
        if (!IsActive)
        {
            return;
        }
        Movement();
        Jumping();
    }
    private void ExtraGravity()
    {
        // ������ ���������� gravity ���� Vector3
        // ����������� �� �������� ���� ���������� �� ����������� ������ Physics
        Vector3 gravity = Physics.gravity;

        // �������� ���������� �� ��������� � ����� �����
        gravity *= _extraGravityMultiplier * Time.fixedDeltaTime;

        // ���������� ���������� � ������� �������� ������
        _rigidbody.velocity += gravity;
    }

    private void Movement()
    {
        // ������ ���������� movement �� ��������� (0, 0, 0)
        Vector3 movement = Vector3.zero;

        // ����� movement.x �������� ��������������� ����� � ���������� (������� A � D)
        movement.x = Input.GetAxis(MovementHorizontalKey);

        // ���� ������ �������� ������ 1, ����������� ���
        // ����� �������� �������� �������� �� ���������
        movement = Vector3.ClampMagnitude(movement, 1f);

        // �������� ������ �������� �� �������� 
        movement *= _movementSpeed;

        // ����� ����� �������� �� X
        // ��� ���� �������� �������� �� Z
        // ��� ��� �������������� ���������� ��������
        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ������ ���� ����� ��������
        _canJump = true;

        // ������ ���� ��� �������
        _isJumping = false;
    }

    private void OnCollisionStay(Collision collision)
    {

        // ��������� �� �� ��������
        // ��� � � ������ ������������
        _canJump = true;
        _isJumping = false;
    }
    private void Jumping()
    {
        // ���� ������ ������� �������
        // � ����� ����� �������
        // � ������ ������ �� �����������
        if (Input.GetKeyDown(KeyCode.Space) && _canJump && !_isJumping)
        {
            // ������ ���� ��� ���� ��������
            _canJump = false;

            // ������ ���� ��������
            _isJumping = true;

            // �������� ������ ������
            _jumpTimer = 0;

            // ����� ������������ �������� ��� ������
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpForce, 0f);
        }
        // ���� ����� � ������
        if (_isJumping)
        {
            // ����������� ������ ������
            _jumpTimer += Time.fixedDeltaTime;

            // ���� ������������ ������ ���������
            if (_jumpTimer >= _jumpDuration)
            {
                // ������ ���� ��� �������
                _isJumping = false;
            }
        }
    }
    private void Update()
    {
        // ���� ����� �� �������
        if (!PhotonView || !PhotonView.IsMine || !IsActive)
        {
            // ������� �� ������
            return;
        }
        // �������� ����� LookRotation()
        LookRotation();
    }

    private void LookRotation()
    {
        // ������������ ������� ������
        // �� ������� ��������� � ��������
        Vector3 playerOnScreenPosition = _mainCamera.WorldToScreenPoint(_rigidbody.position);

        // ��������� ����������� ������� ������
        // ������������ ������� ����
        float lookSign = Mathf.Sign(Input.mousePosition.x - playerOnScreenPosition.x);

        // ����� ���� �������� ������� ������
        float lookYEuler = lookSign * 90;

        // ������������ ������ �� ���� lookYEuler ������ ��� Y
        _rigidbody.rotation = Quaternion.Euler(0f, lookYEuler, 0f);
    }
}

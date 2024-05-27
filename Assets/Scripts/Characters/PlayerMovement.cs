using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    // Константа с ключом горизонтального движения
    // Будем использовать её для получения данных ввода с клавиатуры
    private const string MovementHorizontalKey = "Horizontal";

    // Множитель гравитации
    [SerializeField] private float _extraGravityMultiplier = 2f;

    // Скорость движения
    [SerializeField] private float _movementSpeed = 20f;

    // Сила прыжка
    [SerializeField] private float _jumpForce = 45f;

    // Длительность прыжка
    [SerializeField] private float _jumpDuration = 1f;

    // Компонент Rigidbody на физическом объекте
    private Rigidbody _rigidbody;

    // Главная камера
    private Camera _mainCamera;

    // Флаг того, что герой может прыгать
    private bool _canJump;

    // Флаг того, что герой в прыжке
    private bool _isJumping;

    // Таймер длительности прыжка
    private float _jumpTimer;

    protected override void OnInit()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _mainCamera = Camera.main;

        if (!PhotonView || !PhotonView.IsMine)
        {
            // Уничтожаем компонент физики на персонаже
            // Так как физика врага будет работать на его клиенте
            Destroy(_rigidbody);
        }
    }

    private void FixedUpdate()
    {
        if (!PhotonView || !PhotonView.IsMine || !_rigidbody)
        {
            // НОВОЕ: Выходим из метода
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
        // Создаём переменную gravity типа Vector3
        // Присваиваем ей значение силы гравитации из физического движка Physics
        Vector3 gravity = Physics.gravity;

        // Умножаем гравитацию на множитель и время кадра
        gravity *= _extraGravityMultiplier * Time.fixedDeltaTime;

        // Прибавляем гравитацию к текущей скорости игрока
        _rigidbody.velocity += gravity;
    }

    private void Movement()
    {
        // Вводим переменную movement со значением (0, 0, 0)
        Vector3 movement = Vector3.zero;

        // Задаём movement.x значение горизонтального ввода с клавиатуры (клавиши A и D)
        movement.x = Input.GetAxis(MovementHorizontalKey);

        // Если вектор движения больше 1, нормализуем его
        // Чтобы избежать быстрого движения по диагонали
        movement = Vector3.ClampMagnitude(movement, 1f);

        // Умножаем вектор движения на скорость 
        movement *= _movementSpeed;

        // Задаём новую скорость по X
        // При этом обнуляем скорость по Z
        // Так как предполагается двухмерное движение
        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ставим флаг «могу прыгать»
        _canJump = true;

        // Ставим флаг «не прыгаю»
        _isJumping = false;
    }

    private void OnCollisionStay(Collision collision)
    {

        // Повторяем те же действия
        // Что и в начале столкновения
        _canJump = true;
        _isJumping = false;
    }
    private void Jumping()
    {
        // Если нажата клавиша «пробел»
        // И герой может прыгать
        // И прыжок сейчас не выполняется
        if (Input.GetKeyDown(KeyCode.Space) && _canJump && !_isJumping)
        {
            // Ставим флаг «не могу прыгать»
            _canJump = false;

            // Ставим флаг «прыгаю»
            _isJumping = true;

            // Обнуляем таймер прыжка
            _jumpTimer = 0;

            // Задаём вертикальную скорость для прыжка
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpForce, 0f);
        }
        // Если герой в прыжке
        if (_isJumping)
        {
            // Увеличиваем таймер прыжка
            _jumpTimer += Time.fixedDeltaTime;

            // Если длительность прыжка превышена
            if (_jumpTimer >= _jumpDuration)
            {
                // Ставим флаг «не прыгаю»
                _isJumping = false;
            }
        }
    }
    private void Update()
    {
        // Если игрок не активен
        if (!PhotonView || !PhotonView.IsMine || !IsActive)
        {
            // Выходим из метода
            return;
        }
        // Вызываем метод LookRotation()
        LookRotation();
    }

    private void LookRotation()
    {
        // Конвертируем позицию игрока
        // Из мировых координат в экранные
        Vector3 playerOnScreenPosition = _mainCamera.WorldToScreenPoint(_rigidbody.position);

        // Вычисляем направление взгляда игрока
        // Относительно курсора мыши
        float lookSign = Mathf.Sign(Input.mousePosition.x - playerOnScreenPosition.x);

        // Задаём угол поворота взгляда игрока
        float lookYEuler = lookSign * 90;

        // Поворачиваем игрока на угол lookYEuler вокруг оси Y
        _rigidbody.rotation = Quaternion.Euler(0f, lookYEuler, 0f);
    }
}

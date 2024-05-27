using UnityEngine;

// Наследуем PlayerAiming от CharacterAiming
public class PlayerAiming : CharacterAiming
{
    // Главная камера
    private Camera _mainCamera;

    // Инициализируем переменные
    protected override void OnInit()
    {
        // Инициализируем объект базового класса
        // То есть CharacterAiming
        base.OnInit();

        // Присваиваем _mainCamera объект камеры
        _mainCamera = Camera.main;
    }
    // Вызывается каждый кадр
    private void Update()
    {
        // Если игрок не активен
        if (!PhotonView || !PhotonView.IsMine || !IsActive)
        {
            // Выходим из метода
            return;
        }
        // Вызываем метод Aiming()
        Aiming();
    }
    // Управляем прицеливанием игрока
    private void Aiming()
    {
        // Вычисляем разницу в координате Z
        // Между персонажем и камерой
        float characterZDelta = transform.position.z - _mainCamera.transform.position.z;

        // Преобразуем позицию курсора мыши
        // Из экранных координат в мировые
        Vector3 mouseInWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * characterZDelta);

        // Поворачиваем оружие в сторону курсора 
        Weapon.transform.LookAt(mouseInWorldPosition);
    }
}
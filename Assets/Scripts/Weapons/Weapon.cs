using System;
using UnityEngine;
using Photon.Pun;

public abstract class Weapon : MonoBehaviour, IShootCountBonusDependent, IHitTypeBonusDependent
{

    [SerializeField] private int _damage = 10;

    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private float _bulletDelay = 0.05f;

    [SerializeField] private int _bulletsInRow = 7;

    [SerializeField] private float _reloadingDuration = 4f;

    private Transform _bulletSpawnPoint;

    private int _currentBulletsInRow;

    private float _bulletTimer;

    private float _reloadingTimer;

    private bool _isShootDelayEnd;

    private bool _isReloading;

    public Action<int, int> OnBulletsInRowChange;

    public Action OnEndReloading;

    private int _shootCount;

    private BulletHit _bulletHit;

    public bool IsReloading => _isReloading;
    public void Init()
    {
        // Получаем компонент Transform для точки вылета пули
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;

        // Вызываем метод FillBulletsToRow()
        FillBulletsToRow();
    }

    public void SetActive(bool value)
    {
        // Меняем активность оружия на value
        gameObject.SetActive(value);

        // Вызываем событие OnBulletsInRowChange
        // Передаём в него текущее и начальное число пуль
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }
    public void Shoot()
    {
        // Если задержка между выстрелами не закончилась
        // Или в магазине нет пуль
        if (!_isShootDelayEnd || !CheckHasBulletsInRow())
        {
            // Выходим из метода
            return;
        }
        // Обнуляем таймер выстрела
        _bulletTimer = 0;

        // Вызываем метод DoShoot()
        DoShoot();

        // Уменьшаем количество пуль
        _currentBulletsInRow--;

        // Вызываем событие OnBulletsInRowChange
        // Передаём в него текущее и начальное число пуль
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }

    public void Reload()
    {
        // Если оружие перезаряжается
        if (_isReloading)
        {
            // Выходим из метода
            return;
        }
        // Ставим флаг перезарядки
        _isReloading = true;
    }
    public bool CheckHasBulletsInRow()
    {
        // Вычисляем текущее количество пуль
        // Если оно > 0, возвращаем true
        return _currentBulletsInRow > 0;
    }

    protected void DoShoot()
    {
        for (int i = 0; i < _shootCount; i++)
        {
            SpawnBullet(_bulletPrefab, _bulletSpawnPoint, GetShootAngle(i, _shootCount));
        }
    }

    private void SpawnBullet(Bullet prefab, Transform spawnPoint, float extraAngle)
    {
        GameObject buleltGO = PhotonNetwork.Instantiate(prefab.name, spawnPoint.position, spawnPoint.rotation);

        Bullet bullet = buleltGO.GetComponent<Bullet>();

        Vector3 bulletEulerAngles = bullet.transform.eulerAngles;
        bulletEulerAngles.x += extraAngle;
        bullet.transform.eulerAngles = bulletEulerAngles;
        bullet.Init(_damage, _bulletHit);
    }

    private void Update()
    {
        // Вызываем метод ShootDelaying()
        ShootDelaying();

        // Вызываем метод Reloading()
        Reloading();
    }

    private void ShootDelaying()
    {
        // Увеличиваем таймер выстрела
        // На время, прошедшее с последнего кадра
        _bulletTimer += Time.deltaTime;

        // Присваиваем _isShootDelayEnd значение
        // В зависимости от того, прошла ли задержка
        _isShootDelayEnd = _bulletTimer >= _bulletDelay;
    }
    private void Reloading()
    {
        // Если оружие перезаряжается
        if (_isReloading)
        {
            // Увеличиваем таймер перезарядки
            // На время, прошедшее с последнего кадра
            _reloadingTimer += Time.deltaTime;

            // Если прошла продолжительность перезарядки
            if (_reloadingTimer >= _reloadingDuration)
            {
                // Вызываем метод FillBulletsToRow()
                FillBulletsToRow();

                // Вызываем событие OnEndReloading
                OnEndReloading?.Invoke();
            }
        }
    }

    private void FillBulletsToRow()
    {
        // Снимаем флаг перезарядки
        _isReloading = false;

        // Обнуляем таймер перезарядки
        _reloadingTimer = 0;

        // Задаём текущее количество пуль
        _currentBulletsInRow = _bulletsInRow;

        // Вызываем событие OnBulletsInRowChange
        // Передаём в него текущее и начальное число пуль
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }

    public void SetShootCount(int value)
    {
        _shootCount = value;
    }

    private float GetShootAngle(int shootId, int shootCount)
    {
        float startAngle = 0;

        float stepAngle = 0;

        switch (shootCount)
        {
            case 2:

                startAngle = -3;

                stepAngle = 6;

                break;

            case 3:

                startAngle = -5;

                stepAngle = 5;

                break;

            case 4:

                startAngle = -6;

                stepAngle = 4;

                break;

            default:

                return 0;
        }

        return startAngle + stepAngle * shootId;
    }

    public void SetHit(BulletHit hit)
    {
        _bulletHit = hit;
    }

}

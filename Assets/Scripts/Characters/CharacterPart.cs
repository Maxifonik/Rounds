using Photon.Pun;

// Сделали класс абстрактным
public abstract class CharacterPart : MonoBehaviourPunCallbacks
{
    // Флаг активности части
    protected bool IsActive;

    // Переменная для работы с сетевым представлением объекта
    private PhotonView _photonView;

    // Свойство для доступа к _photonView
    // Из дочерних классов
    protected PhotonView PhotonView
    {
        // Получаем значение переменной
        get
        {
            // Если _photonView не инициализирована
            if (!_photonView)
            {
                // Получаем компонент PhotonView
                _photonView = GetComponent<PhotonView>();
            }
            // Возвращаем полученное значение
            return _photonView;
        }
    }

    // Инициализируем переменные
    public void Init()
    {
        // Делаем часть активной
        IsActive = true;

        // Вызываем метод OnInit()
        OnInit();
    }
    // Останавливаем часть персонажа
    public void Stop()
    {
        // Делаем часть неактивной
        IsActive = false;

        // Вызываем метод OnStop()
        OnStop();
    }
    // Защищённый виртуальный метод OnInit()
    protected virtual void OnInit() { }

    // Защищённый виртуальный метод OnStop()
    protected virtual void OnStop() { }
}

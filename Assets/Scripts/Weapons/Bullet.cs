using UnityEngine;
using Photon.Pun;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float _impulse = 30f;

    [SerializeField] private float _lifeTime = 15f;

    private PhotonView _photonView;

    // Поведение при попадании
    private BulletHit _hit;


    private void Update()
    {
        // НОВОЕ: Если у игрока нет PhotonView
        if (!_photonView.IsMine)
        {
            return;
        }
        ReduceLifeTime();
    }

    private void ReduceLifeTime()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    public void Init(int damage, BulletHit hit)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * _impulse, ForceMode.Impulse);

        _hit = hit;

        hit.Init(damage, _impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // НОВОЕ: Если у игрока нет PhotonView
        if (!_photonView.IsMine)
        {
            return;
        }
        if (collision.collider)
        {
            _hit.Hit(collision, transform);
            DestroyBullet();
        }
    }
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        // Если у игрока его нет
        if (!_photonView.IsMine)
        {
            // Уничтожаем компонент физики на персонаже
            // Чтобы физика пули отрабатывалась на клиенте, который создал пулю
            // Её позиция будет синхронизироваться с другими клиентами
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
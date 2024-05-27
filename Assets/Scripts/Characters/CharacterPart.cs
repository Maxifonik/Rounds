using Photon.Pun;

// ������� ����� �����������
public abstract class CharacterPart : MonoBehaviourPunCallbacks
{
    // ���� ���������� �����
    protected bool IsActive;

    // ���������� ��� ������ � ������� �������������� �������
    private PhotonView _photonView;

    // �������� ��� ������� � _photonView
    // �� �������� �������
    protected PhotonView PhotonView
    {
        // �������� �������� ����������
        get
        {
            // ���� _photonView �� ����������������
            if (!_photonView)
            {
                // �������� ��������� PhotonView
                _photonView = GetComponent<PhotonView>();
            }
            // ���������� ���������� ��������
            return _photonView;
        }
    }

    // �������������� ����������
    public void Init()
    {
        // ������ ����� ��������
        IsActive = true;

        // �������� ����� OnInit()
        OnInit();
    }
    // ������������� ����� ���������
    public void Stop()
    {
        // ������ ����� ����������
        IsActive = false;

        // �������� ����� OnStop()
        OnStop();
    }
    // ���������� ����������� ����� OnInit()
    protected virtual void OnInit() { }

    // ���������� ����������� ����� OnStop()
    protected virtual void OnStop() { }
}

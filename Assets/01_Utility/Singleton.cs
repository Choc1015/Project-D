using UnityEngine;
using UnityEngine.Rendering;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool quitting = false; // �� �̱����� ��ӹ��� ��ü�� ��������� üũ
    public bool DontDestroy = true;
    public static T Instance
    {
        get
        {
            if (quitting)
            {
                // �̱����� ��ӹ��� ��ü�� ������ٸ� �������� �ʴ´�.
                return null;
            }
            if (instance == null)
            {
                // �ش� ������Ʈ�� ������ �ִ� ���� ������Ʈ�� ã�Ƽ� ��ȯ�Ѵ�.
                instance = (T)FindAnyObjectByType(typeof(T));

                if (instance == null) // �ν��Ͻ��� ã�� ���� ���
                {
                    // ���ο� ���� ������Ʈ�� �����Ͽ� �ش� ������Ʈ�� �߰��Ѵ�.
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    // ������ ���� ������Ʈ���� �ش� ������Ʈ�� instance�� �����Ѵ�.
                    instance = obj.GetComponent<T>();
                }
            }

            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (DontDestroy == false)
        {
            transform.parent = null;
        }
    }
    protected virtual void Start()
    {
        if (DontDestroy == false)
        {
            return;
        }

        if (transform.parent != null && transform.root != null) // �ش� ������Ʈ�� �ڽ� ������Ʈ���
        {
            DontDestroyOnLoad(this.transform.root.gameObject); // �θ� ������Ʈ�� DontDestroyOnLoad ó��
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // �ش� ������Ʈ�� �� ���� ������Ʈ��� �ڽ��� DontDestroyOnLoad ó��
        }

    }
    public void ResetSingleton() => instance = null;
    private void OnDestroy()
    {
        // �̱��� ��ü �ı� üũ
        //quitting = true;
    }
}
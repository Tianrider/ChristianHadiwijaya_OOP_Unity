using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int level;

    public UnityEvent enemyKilledEvent;

    private void Start()
    {
        enemyKilledEvent ??= new UnityEvent();
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }

    public void FacePlayer(Transform playerTransform)
    {
        if (playerTransform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        enemyKilledEvent.Invoke();
    }
}

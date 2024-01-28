using UnityEngine;

public class LevelWinTrigger : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Item>(out var _))
        {
            GameManager.Instance.OnWin();
        }
    }
}
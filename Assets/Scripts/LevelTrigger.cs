using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public int level;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Item>(out var _))
        {
            GameManager.Instance.OnLevelComplete(this.level);
        }
    }
}
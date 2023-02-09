using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    private void Awake()
    {
        if (enabled)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

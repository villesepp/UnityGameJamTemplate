using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] private GameObject corePrefab;

    [Header("Scene State")]
    [SerializeField] private GameState sceneStartState = GameState.Playing;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            if (corePrefab != null)
            {
                Instantiate(corePrefab);
                Debug.Log("Core prefab instantiated by Bootstrapper.");
            }
            else
            {
                Debug.LogWarning("Bootstrapper is missing a Core prefab reference.");
            }
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(sceneStartState);
        }
    }
}
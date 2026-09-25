using UnityEngine;
using UnityEngine.SceneManagement;

public class DevSceneJump : MonoBehaviour
{

    public string targetSceneName;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        SceneManager.LoadScene(targetSceneName);
    }
}


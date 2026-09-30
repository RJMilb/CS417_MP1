using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Attach to your Player. Keeps the player between scenes and moves them
/// to a spawn point every time a new scene loads.
///
/// Setup:
/// 1. Put this on the Player object.
/// 2. In each scene, create an empty GameObject where the player should appear
///    and add the "PlayerSpawnPoint" tag (Tags & Layers > Add Tag), or just name it
///    to match "spawnPointName" below.
/// 3. To pick a specific spawn (e.g. which door you came through), set
///    PlayerSceneSpawner.NextSpawnName = "DoorB"; before calling SceneManager.LoadScene.
/// </summary>
public class PlayerSceneSpawner : MonoBehaviour
{
    public static PlayerSceneSpawner Instance { get; private set; }

    /// <summary>Set this before loading a scene to choose a specific spawn point by name.</summary>
    public static string NextSpawnName;

    [Header("Spawn Settings")]
    [SerializeField] private string spawnPointTag = "PlayerSpawnPoint";
    [SerializeField] private string defaultSpawnName = "PlayerSpawn";
    [SerializeField] private bool matchSpawnRotation = true;

    private CharacterController characterController;
    private Rigidbody rb;

    private void Awake()
    {
        // Prevent duplicate players when returning to a scene that already contains one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != this) return;

        Transform spawn = FindSpawnPoint();
        if (spawn == null)
        {
            Debug.LogWarning($"No spawn point found in scene '{scene.name}'. Player left where it is.");
            return;
        }

        TeleportTo(spawn);
        NextSpawnName = null; // reset so it doesn't affect later loads
    }

    private Transform FindSpawnPoint()
    {
        // 1. Specific spawn requested (e.g. a particular door)
        if (!string.IsNullOrEmpty(NextSpawnName))
        {
            GameObject named = GameObject.Find(NextSpawnName);
            if (named != null) return named.transform;
        }

        // 2. Tagged spawn point
        try
        {
            GameObject tagged = GameObject.FindWithTag(spawnPointTag);
            if (tagged != null) return tagged.transform;
        }
        catch (UnityException)
        {
            // Tag doesn't exist in project; fall through to name lookup
        }

        // 3. Default name
        GameObject fallback = GameObject.Find(defaultSpawnName);
        return fallback != null ? fallback.transform : null;
    }

    private void TeleportTo(Transform spawn)
    {
        // CharacterController overrides transform changes unless disabled first
        if (characterController != null) characterController.enabled = false;

        transform.position = spawn.position;
        if (matchSpawnRotation) transform.rotation = spawn.rotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;      // use rb.velocity on Unity 2022 and older
            rb.angularVelocity = Vector3.zero;
        }

        if (characterController != null) characterController.enabled = true;
    }
}
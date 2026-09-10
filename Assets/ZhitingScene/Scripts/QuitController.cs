using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitController : MonoBehaviour
{
    public InputActionReference quitAction;

    void OnEnable()
    {
        quitAction.action.Enable();
        quitAction.action.performed += QuitTheGame;
    }

    void OnDisable()
    {

        quitAction.action.performed -= QuitTheGame;
        quitAction.action.Disable();
    }

    private void QuitTheGame(InputAction.CallbackContext obj)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif

        OnDisable();
    }

}

using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Light light;
    public InputActionReference action;

    void Start()
    {
        light = GetComponent<Light>();

        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            light.color = new Color(Random.value, Random.value, Random.value);
        };
    }


    // Update is called once per frame
    void Update()
    {

    }
}

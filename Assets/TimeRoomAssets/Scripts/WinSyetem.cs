using UnityEngine;
using System.Collections;
using TMPro;

public class WinSyetem : MonoBehaviour
{
    public GameObject light;
    public TextMeshProUGUI winText;
    public float duration = 2f;


    void Start()
    {
        if (light != null) light.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class LogButton : MonoBehaviour
{
    private UnityEngine.UI.Button _button;
    void Start()
    {
        _button = GetComponent <UnityEngine.UI.Button>();
        _button.onClick.AddListener(() => { Debug.Log(name); });
    }
    
    
}

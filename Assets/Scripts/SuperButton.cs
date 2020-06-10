using System.Collections.Generic;
using UnityEngine;

public class SuperButton : MonoBehaviour
{
    [SerializeField] private GameObject content;
    private bool _isActive;

    private static List<SuperButton> _instanceList = new List<SuperButton>();

    void Start()
    {
        _instanceList.Add(this);
        SetActiveContent(false);
    }

    public void ToggleActivateContent()
    {
        bool isActive = _isActive;
        DeactivateAllContents();
        SetActiveContent(!isActive);
    }

    private static void DeactivateAllContents()
    {
        foreach (var superButton in _instanceList)
        {
            superButton.SetActiveContent(false);
        }
    }

    private void SetActiveContent(bool activate)
    {
        _isActive = activate;
        content.SetActive(activate);
    }
}

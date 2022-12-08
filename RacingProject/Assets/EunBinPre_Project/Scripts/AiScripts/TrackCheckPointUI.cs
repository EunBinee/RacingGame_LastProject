using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPointUI : MonoBehaviour
{
    [SerializeField] private GameObject warningUI;

    void Start()
    {
    }


    public void Show()
    {
        warningUI.SetActive(true);
    }
    public void Hide()
    {
        warningUI.SetActive(false);
    }

}

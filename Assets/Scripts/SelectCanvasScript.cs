using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCanvasScript : MonoBehaviour {

    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GameObject selectedObject;

    private bool buttonSelected;

    public void ReEnableMenu()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        buttonSelected = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenSelectSound : MonoBehaviour
{
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private AudioSource selectedSound;
    [SerializeField]
    private AudioClip selected;
    private int firstSelected;
    void Start () {
        firstSelected = 0;
	}

	public void PlaySelectSound ()
    {
        if (eventSystem.firstSelectedGameObject.name == this.name && firstSelected == 0)
        {
            firstSelected++;
        }
        else
        {
            selectedSound.PlayOneShot(selected);
        }
	}
}

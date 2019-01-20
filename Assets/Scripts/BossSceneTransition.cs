using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossSceneTransition : MonoBehaviour {

    [SerializeField]
    private Image image;
    private Color tempColor;
    [SerializeField]
    private AudioSource musicBox;
    private float musicBoxVolume;


    [SerializeField]
    private GameObject boss;
    private BossScriptTest bossScript;
    
    [SerializeField]
    private float transitionTime;
    private float countdownTime;
    [SerializeField]
    private string nextScene;

    // Use this for initialization
    void Start ()
    {
        countdownTime = transitionTime;
        bossScript = boss.GetComponent<BossScriptTest>();

        tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
        musicBoxVolume = musicBox.volume;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (bossScript.hp <= 0)
        {
            SceneTransition();
        }
	}

    private void SceneTransition()
    {
        countdownTime -= Time.deltaTime;
        if (countdownTime + 3 <= 3)
        {
            if (tempColor.a <= 1f)
            {
                tempColor.a += 0.33f * Time.deltaTime;
            }
            musicBox.volume -= (musicBoxVolume/3f)*Time.deltaTime;
            image.color = tempColor;
        }
        if (countdownTime + 3 <= 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

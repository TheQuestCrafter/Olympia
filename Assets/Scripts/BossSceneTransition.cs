using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneTransition : MonoBehaviour {

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
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (bossScript.hp <= 0)
        {
            SceneTransition();
        }
	}

    private void SceneTransition()
    {
        countdownTime -= Time.deltaTime;
        if (countdownTime <= 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

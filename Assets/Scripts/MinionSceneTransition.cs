using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinionSceneTransition : MonoBehaviour {

    [SerializeField]
    private GameObject spawner;
    private EnemySpawner spawnerScript;

    [SerializeField]
    private float transitionTime;
    private float countdownTime;
    [SerializeField]
    private string nextScene;

    // Use this for initialization
    void Start()
    {
        countdownTime = transitionTime;
        spawnerScript = spawner.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerScript.sceneTimeLeft <= 0)
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

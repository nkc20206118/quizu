using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text scoretext;
    public GameObject gameovertext;
    int point = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameovertext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "score: " + point.ToString("D6");

        if (gameovertext.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Quit();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("2");
                SceneManager.LoadScene("SampleScene");
            }
        }

    }

    public void addpoint()
    {
        point += 100;
    }

    public void GameOver()
    {
        gameovertext.SetActive(true);
    }

    //終了
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}

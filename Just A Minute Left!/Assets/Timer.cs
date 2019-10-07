using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int LoadedScreen;
    public List<GameObject>DisabledUI;

    public TextMeshProUGUI timer;
    public int time = 60;

    public int timeLeft;

    public bool hasTimer;

    public float EndScreenLength;

    public float endScreenLength = 7;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = time;
        tf = time;
    }

    public float tf;
    bool end;
    float et;
    public bool paused;
    // Update is called once per frame
    void Update()
    {
        if (hasTimer)
        {
            timer.text = timeLeft.ToString();
        }
        else
        {
            timer.text = "";
        }
        if (paused)
        {
            return;
        }
        if (end)
        {
            et += Time.unscaledDeltaTime;
            if (et >= endScreenLength)
            {
                Time.timeScale = 1;
                if (GetComponent<Interact>().items.Contains("Password"))
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(LoadedScreen);
                }
            }
            return;
        }
        tf -= Time.deltaTime;
        if ((int) tf != timeLeft)
        {
            //tick noise
            timeLeft = (int) tf;
        }
        if (timeLeft <= 0)
        {
            GameObject.Find("EndScreen").GetComponent<Animator>().SetTrigger("TimeUp");
            Time.timeScale = 0;
            foreach(GameObject Ui in DisabledUI)
            {
                Ui.SetActive(false);
            }
            end = true;
        }
    }
}

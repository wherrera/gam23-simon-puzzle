using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject blockInput;
    public Text currentLevelText;

    public Button red;
    public Button yellow;
    public Button green;
    public Button blue;

    public GameObject win;
    public GameObject lose;

    Button[] buttons;

    int currentLevel;
    List<Button> buttonSequence = new List<Button>();

    int currentUserButtonIndex;

    IEnumerator StartLevel()
    {
        blockInput.SetActive(true);

        //red
        Highlight(red);

        yield return new WaitForSeconds(1f);

        //yellow
        Highlight(yellow);

        yield return new WaitForSeconds(1f);

        //blue
        Highlight(blue);

        yield return new WaitForSeconds(1f);

        //green
        Highlight(green);

        yield return new WaitForSeconds(1f);

        //highlght all
        HighlightAll();

        yield return new WaitForSeconds(1f);

        Debug.Log("Current Level: "+ currentLevel);

        for (int i = 0; i < currentLevel; i++)
        {
            int n = Random.Range(0, 4);

            Debug.Log("Random Number: " + n);

            Highlight(buttons[n]);
            buttonSequence.Add(buttons[n]);
            yield return new WaitForSeconds(1f);

            DimAll();

            yield return new WaitForSeconds(1f);
        }

        blockInput.SetActive(false);
    }

    void DimAll()
    {
        red.image.color = new Color(1f, 0f, 0f, 0.25f);
        yellow.image.color = new Color(1f, 1f, 0f, 0.25f);
        green.image.color = new Color(0f, 1f, 0f, 0.25f);
        blue.image.color = new Color(0f, 0f, 1f, 0.25f);
    }

    void HighlightAll()
    {
        red.image.color = new Color(1f, 0f, 0f, 1f);
        yellow.image.color = new Color(1f, 1f, 0f, 1f);
        green.image.color = new Color(0f, 1f, 0f, 1f);
        blue.image.color = new Color(0f, 0f, 1f, 1f);
    }

    void Highlight(Button button)
    {
        foreach(Button btn in buttons)
        {
            Color color = btn.image.color;

            color.a = btn == button ? 1f : 0.1f;

            btn.image.color = color;
        }
    }

    void OnClickRed()
    {
        if(buttonSequence[currentUserButtonIndex] != red)
        {
            lose.SetActive(true);
            return;
        }

        if(currentUserButtonIndex == buttonSequence.Count-1)
        {
            SetCurrentLevel(currentLevel + 1);

            currentUserButtonIndex = 0;
            buttonSequence.Clear();
            StartCoroutine(StartLevel());
            return;
        }

        currentUserButtonIndex++;
    }

    void OnClickYellow()
    {
        if (buttonSequence[currentUserButtonIndex] != yellow)
        {
            lose.SetActive(true);
            return;
        }

        if (currentUserButtonIndex == buttonSequence.Count - 1)
        {
            SetCurrentLevel(currentLevel + 1);

            currentUserButtonIndex = 0;
            buttonSequence.Clear();
            StartCoroutine(StartLevel());
            return;
        }

        currentUserButtonIndex++;
    }
    
    void OnClickBlue()
    {
        if (buttonSequence[currentUserButtonIndex] != blue)
        {
            lose.SetActive(true);
            return;
        }

        if (currentUserButtonIndex == buttonSequence.Count - 1)
        {
            SetCurrentLevel(currentLevel + 1);
            currentUserButtonIndex = 0;
            buttonSequence.Clear();
            StartCoroutine(StartLevel());
            return;
        }

        currentUserButtonIndex++;
    }

    void OnClickGreen()
    {
        if (buttonSequence[currentUserButtonIndex] != green)
        {
            lose.SetActive(true);
            return;
        }

        if (currentUserButtonIndex == buttonSequence.Count - 1)
        {
            SetCurrentLevel(currentLevel + 1);
            currentUserButtonIndex = 0;
            buttonSequence.Clear();
            StartCoroutine(StartLevel());
            return;
        }

        currentUserButtonIndex++;
    }

    void SetCurrentLevel(int level)
    {
        currentLevel = level;

        currentLevelText.text = "Level\n" + level;
    }

    // Use this for initialization
    void Start ()
    {
        HighlightAll();

        buttons = new Button[]
        {
            red,
            yellow,
            blue,
            green
        };

        red.onClick.AddListener(OnClickRed);
        yellow.onClick.AddListener(OnClickYellow);
        green.onClick.AddListener(OnClickGreen);
        blue.onClick.AddListener(OnClickBlue);

        SetCurrentLevel(1);

        StartCoroutine( StartLevel() );
    }

    public void Restart()
    {
        SetCurrentLevel(1);
        currentUserButtonIndex = 0;
        buttonSequence.Clear();
        lose.SetActive(false);
        StartCoroutine(StartLevel());
    }
}
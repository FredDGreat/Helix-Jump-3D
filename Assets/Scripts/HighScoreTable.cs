using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = transform.Find("HighScoreTemplate");

        entryTemplate.gameObject.SetActive(false);
        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry { score = 834, name = "Obi" },
            new HighscoreEntry { score = PlayerPrefs.GetInt(GameManager.HIGH_SCORE_PREF,0), name = "You" },
            new HighscoreEntry { score = 523, name = "Ikem" },
            new HighscoreEntry { score = 994, name = "Emma" },
            new HighscoreEntry { score = 130, name = "Awa" }
        };
        //sort the list entry by score
        for(int i = 0;i < highscoreEntryList.Count; i++)
        {
            for(int j = i + 1;j < highscoreEntryList.Count; j++)
            {
                if(highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    //swap the score
                    HighscoreEntry temp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = temp;
                }
            }
        }
        highscoreEntryTransformList = new List<Transform>();
        foreach(HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        //float templateHeight = 50f;
        /*for(int i = 0; i < 5; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(entryTemplate.position.x, entryTemplate.position.y - templateHeight * i);
            entryTransform.parent = transform;
            entryTransform.gameObject.SetActive(true);

            int rank = 1 + i;
            string rankString;
            switch (rank)
            {
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
                default: rankString = rank + "TH"; break;
            }
            entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;

            int score = Random.Range(0, 200);
            entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

            string name = "AAA";
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

        }*/

    }
    
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry,Transform container, List<Transform> transformList)
    {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(entryTemplate.position.x, entryTemplate.position.y - templateHeight * transformList.Count);
        entryTransform.parent = transform;
        entryTransform.gameObject.SetActive(true);

        int rank = 1 + transformList.Count;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }
        entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;

        //int score = Random.Range(0, 200);
        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        //string name = "AAA";
        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

    /**
     * Represents a single high score
     */
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}

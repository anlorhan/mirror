using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Database
{
    public Questions[] QQuestions;
}

[Serializable]
public class Questions
{
    public string Question;
    public string Answer_A;
    public string Answer_B;
    public string Answer_C;
    public string Answer_D;
    public string trueAnswer;
}

public class QuizGetter : MonoBehaviour
{
    private Database data;

    [SerializeField] TextMeshProUGUI Question;
    [SerializeField] TextMeshProUGUI Answer_A;
    [SerializeField] TextMeshProUGUI Answer_B;
    [SerializeField] TextMeshProUGUI Answer_C;
    [SerializeField] TextMeshProUGUI Answer_D;


    string jsonURL = "https://anlorhan.github.io/game-json/db.json";//"https://my-json-server.typicode.com/anlorhan/game-json/db";//"https://drive.google.com/uc?export=download&id=1VTPLLKnDnEEwwv5xQtuI8gWxxzW8-GWw";
    public GameObject QuizPanel;
    public TextMeshProUGUI ScoreText;
    public int randomQuestionPicker;
    public int Score;
    public string trueAnswer;


    //public int repeats; Kullancýya ayný sorunun kaç kere geldiðini tut

    private void Start()
    {
        Score=PlayerPrefs.GetInt(nameof(Score), 0);
        ScoreText.text = "Score: "+Score;
    }


    public void GetQuestion()
    {
        Question.text = "Soru Yükleniyor...";
        StartCoroutine(GetData(jsonURL));
        QuizPanel.SetActive(true);
    }

    public IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
       
        yield return request.SendWebRequest();
        
        if (request.isNetworkError || request.isHttpError)
        {
            // error ...

        }
        else
        {
            // success...
            data = JsonUtility.FromJson<Database>(request.downloadHandler.text);
            randomQuestionPicker = Random.Range(0, data.QQuestions.Length);
            print(randomQuestionPicker);
            // print data in UI
            Question.text = data.QQuestions[randomQuestionPicker].Question;
            Answer_A.text = "A) " + data.QQuestions[randomQuestionPicker].Answer_A;
            Answer_B.text = "B) " + data.QQuestions[randomQuestionPicker].Answer_B;
            Answer_C.text = "C) " + data.QQuestions[randomQuestionPicker].Answer_C;
            Answer_D.text = "D) " + data.QQuestions[randomQuestionPicker].Answer_D;
            trueAnswer = data.QQuestions[randomQuestionPicker].trueAnswer;
        }

        // Clean up any resources it is using.
        request.Dispose();
    }

    public void Check(string answer)
    {
        if (trueAnswer==answer)
        {
            QuizPanel.SetActive(false);
            Score += 10;
            PlayerPrefs.SetInt(nameof(Score), Score);
            ScoreText.text = "Score: " + Score;
        }
        else
        {
            QuizPanel.SetActive(false);
        }
    }

    


}
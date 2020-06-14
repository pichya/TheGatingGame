using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[System.Serializable]
public class QuestionData 
{
    public string questionText;
    public AnswerData[] answers;
}

[System.Serializable]
public class AnswerData 
{
    public string answerText;
    public bool isCorrect;

}

[System.Serializable]
public class RoundData 
{
    public string name;
    public int timeLimitInSeconds;
    public int pointsAddedForCorrectAnswer;
    public QuestionData[] questions;

}

public class DataController : MonoBehaviour 
{
    public RoundData[] allRoundData;


    // Use this for initialization
    void Start ()  
    {
        DontDestroyOnLoad (gameObject);

        SceneManager.LoadScene ("MenuScreen");
    }

	public RoundData GetCurrentRoundData(int roundNum)
    {
		return allRoundData [roundNum];
    }

    // Update is called once per frame
    void Update () {

    }
}

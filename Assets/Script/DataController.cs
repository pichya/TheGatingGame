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
public class PassengerData 
{
    public string bookingNumber;
	public string frequentflyerID;
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
	public string bookingNumberData;
	public string frequentflyerData;

    // Use this for initialization
    void Start ()  
    {
        DontDestroyOnLoad (gameObject);
		bookingNumberData = "";
		frequentflyerData = "";
        SceneManager.LoadScene ("MenuScreen");
    }

	public RoundData GetCurrentRoundData(int roundNum)
    {
		return allRoundData [roundNum];
    }

	public string GetCurrentPassengerData()
	{
		return bookingNumberData;
	}

	public void SetCurrentPassengerData(string bn, string ff)
	{
		bookingNumberData = bn;
		frequentflyerData = ff;
	}

    // Update is called once per frame
    void Update () {

    }
}

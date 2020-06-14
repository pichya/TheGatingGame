using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public Text questionDisplayText;
    public Text scoreDisplayText;
	public Text bookingDisplayText;
    public Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;

    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private float playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
	
	public Image timerBar;
	public float maxTime = 5f;
	float timeLeft;

	public AudioSource audioSource;
	public AudioClip winSound;
	public AudioClip loseSound;
	public AudioClip roundOverSound;
	public AudioClip clickSound;
	public AudioClip timeoutSound;
	
    // Use this for initialization
    void Start () 
    {
        dataController = FindObjectOfType<DataController> ();
        currentRoundData = dataController.GetCurrentRoundData(dataController.GetCurrentRound());
        questionPool = currentRoundData.questions;

		bookingDisplayText.text = "Booking:"+ dataController.GetCurrentPassengerData();
		
        playerScore = 0;
        questionIndex = 0;

        ShowQuestion ();
        isRoundActive = true;

		ResetTimer();
    }
	
	private void ResetTimer(){
		// Reset timer
		timeLeft = maxTime;
	}
	
	private void playClip(AudioClip audioSound){
		audioSource.clip = audioSound;
		audioSource.Play();
	}

    private void ShowQuestion()
    {
        ResetTimer();
		//
		RemoveAnswerButtons ();
        QuestionData questionData = questionPool [questionIndex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++) 
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0) 
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect) 
        {
            float questionScore = currentRoundData.pointsAddedForCorrectAnswer * timeLeft;
			playerScore += questionScore;
            scoreDisplayText.text = "SCORE: " + playerScore.ToString("F2");
			playClip(winSound);
			NextQuestion();
        }else{
			timeLeft-=1.5f;;
			playClip(loseSound);
		}

    }

	public void NextQuestion(){
		if (questionPool.Length > questionIndex + 1) {
            questionIndex++;
            ShowQuestion ();
        } else 
        {
            EndRound();
			playClip(roundOverSound);
        }
	}
	
    public void EndRound()
    {
        isRoundActive = false;

        questionDisplay.SetActive (false);
        roundEndDisplay.SetActive (true);
    }

    public void ReturnToMenu()
    {
        playClip(clickSound);
		SceneManager.LoadScene ("MenuScreen");
    }

    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round (timeRemaining).ToString ();
    }

    // Update is called once per frame
    void Update () 
    {
        if (isRoundActive) 
        {
			if (timeLeft > 0){
				timeLeft -= Time.deltaTime;
				timerBar.fillAmount = timeLeft/maxTime;
			}else{
				playClip(timeoutSound);
				NextQuestion();
			}

        }
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreenController : MonoBehaviour {
	
	private DataController dataController;
		
	public GameObject LoginObj;
	public GameObject MenuObj;
	public GameObject DenyObj;
	
	public Text BNtext;
	public Text FFtext;
	
	public AudioSource audioSource;
	public AudioClip stingerSound;
	public AudioClip denySound;
	public AudioClip clickSound;
	
	void Start(){
		dataController = FindObjectOfType<DataController> ();
		
		if (dataController.GetCurrentPassengerData().Length == 0){
			Debug.Log("String Length :" + dataController.GetCurrentPassengerData().Length);
			MenuObj.SetActive(false);
			LoginObj.SetActive(true);
		}else{
			ShowMenu();
		}
		
	}
	
	private void playClip(AudioClip audioSound){
		audioSource.clip = audioSound;
		audioSource.Play();
	}
	
	private void SubmitInfo(){
		if (BNtext.text.Length == 6){
			 dataController.SetCurrentPassengerData (BNtext.text, FFtext.text);
			 ShowMenu();
		}else{
			playClip(clickSound);
			DenyObj.SetActive(true);
		}
	}
	
	public void ShowMenu(){
		playClip(stingerSound);
		DenyObj.SetActive(false);
		MenuObj.SetActive(true);
		LoginObj.SetActive(false);
	}
	
    public void StartGame(int roundNumber)
    {
        dataController.SetCurrentRound(roundNumber);
		SceneManager.LoadScene("Game");
		playClip(clickSound);
    }
}
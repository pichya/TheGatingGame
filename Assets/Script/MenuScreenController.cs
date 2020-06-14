using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreenController : MonoBehaviour {
	
	private DataController dataController;
		
	public GameObject LoginObj;
	public GameObject MenuObj;
	
	public Text BNtext;
	public Text FFtext;
	
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
	
	public void SubmitInfo(){
		if (BNtext.text.Length == 6){
			 dataController.SetCurrentPassengerData (BNtext.text, FFtext.text);
			 ShowMenu();
		}
	}
	
	public void ShowMenu(){
		MenuObj.SetActive(true);
		LoginObj.SetActive(false);
	}
	
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
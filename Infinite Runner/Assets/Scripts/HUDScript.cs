using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour {

    float playerScore = 0;
    public Text scoreText;
	
	void Update () {
        playerScore += Time.deltaTime;
        scoreText.text = "Score: " + (int)(playerScore * 100);

    }

    public void IncreaseScore(int amount)
    {
        playerScore += amount;
    }
    
    void OnDisable()
    {
        PlayerPrefs.SetInt("Score", (int)(playerScore * 100));
    }
}

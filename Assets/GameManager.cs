using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool GameEnded = false;

	void Update () {
        if (GameEnded)
            return;

        if (PlayerStats.Lives <= 0) {
            EndGame();
        }
	}

    void EndGame() {
        GameEnded = true;
        Debug.Log("Game over!");
    }
}

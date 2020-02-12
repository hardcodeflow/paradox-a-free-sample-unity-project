using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum GameState{
    InMainScreen=0,
    InGame=1,
    GameOver=2,
    EndGame=3
}
public class GameManager : MonoBehaviour
{

    public bool startGame;
    public GameObject mainScreen;
    public GameObject gameOverScreen;
    public GameState gameState=GameState.InMainScreen;
    public int score;
    public TextMeshProUGUI scoreText;
    private void Start()
    {
        scoreText.text = score.ToString();

        References.soundManager.PlayMusic(SoundId.MainMusic);
        if (startGame) {
            StartGame();
        }
    }
    public void ShowAchievements()
    {
        References.playServices.ShowAchievementsUI();

    }

    public void ShowLeaderBoard()
    {
        References.playServices.ShowLeaderBoardUI();

    }
    public void GiveAPoint() {
        if (gameState == GameState.InGame)
        {
            score++;
            if (score >= 8)
            {
                References.playServices.UnlockAchievement(GPGSIds.achievement_code_writer_junior);

            }

            if (score >= 32)
            {
                References.playServices.UnlockAchievement(GPGSIds.achievement_code_writer_junior_plus);

            }
            if (score >= 224)
            {
                References.playServices.UnlockAchievement(GPGSIds.achievement_code_wirter_sinior);

            }
            if (score >= 428)
            {
                References.playServices.UnlockAchievement(GPGSIds.achievement_code_writer_hacker);

            }

            if (score >= 1024)
            {
                References.playServices.UnlockAchievement(GPGSIds.achievement_code_writer_hacker_plus);

            }
            if (score >= 2048)
            {
                References.playServices.UnlockAchievement(GPGSIds.achievement_code_writer_hacker_plus_plus);

            }

            if (score >= 8048)
            {
                References.playServices.UnlockAchievement(GPGSIds.achievement_god);

            }
            scoreText.text = score.ToString();
            References.codeText.WriteCode();
            References.camera.GivePointEffec();
        }
    }
    public void StartGame() {
        mainScreen.SetActive(false);
        References.soundManager.PlayMusic(SoundId.InGameMusic);
        References.codeText.ResetCode();
        gameState = GameState.InGame;
        References.camera.GoToTarget();
     //   References.enemySpawner.
       // References.enemySpawner.
    }
    public void GameOver() {
      
        References.camera.GameOver();
        References.soundManager.ReducePitch();

        gameState = GameState.GameOver;
        gameOverScreen.SetActive(true);
    }
    public void RestartGame()
    {

        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var g in enemy)
        {
            //Destroy(g.gameObject.GetComponent<EnemyController>());
            g.SendMessage("Die");
            // g.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,1)+0.1f,Random.Range(-1,1)+0.1f)*Random.Range(56,78),ForceMode2D.Impulse);
            Destroy(g.gameObject, 6);
        }
        References.playServices.AddScoreToLeaderboard(GPGSIds.leaderboard_best_code_writer,score);
        score = 0;
        scoreText.text = score.ToString();
        References.soundManager.IncreasePitch();
        References.codeText.ResetCode();
        gameOverScreen.SetActive(false);
        gameState = GameState.InGame;
        References.camera.GoToTarget();
        References.player.RestartGame();
        References.camera.RestartGame();

        //   References.player.res
        //   References.enemySpawner.
        // References.enemySpawner.
    }

}

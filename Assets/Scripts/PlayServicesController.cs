using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayServicesController : MonoBehaviour
{
    public int counter;
    public Text logText;
    private static PlayServicesController instance = null;
    static bool signined;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        if (instance != this)
        {

            Destroy(gameObject);
        }
        if (!signined)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            SignIn();
            signined = true;
        }
    }

    internal void SignIn()
    {

        Social.localUser.Authenticate(success => { });
    }

    #region Achivements

    internal void UnlockAchievement(string achievementId)
    {

        Social.ReportProgress(achievementId, 100, success => { });

    }
    public void IncrementAchievement(string achievementId, int stepsToIncrement)
    {

        //Social.ReportProgress(achievementId, 100, success => { });
        PlayGamesPlatform.Instance.IncrementAchievement(achievementId, stepsToIncrement, success => { });

    }

    public void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();

    }


    #endregion /Achivements



    #region LeaderBoard
    internal void AddScoreToLeaderboard(string leaderboardId, long score)
    {

        Social.ReportScore(score, leaderboardId, success => { });
    }
    public void ShowLeaderBoardUI()
    {

        Social.ShowLeaderboardUI();
    }

    #endregion /LeaderBoard
    void Update()
    {

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public string SavePath;
    public NetworkManager networkManager;
    public WorldManager worldManager;
    private int score;
    public int maxHealth;
    private int health;

    public GameData data;

    public Action OnUserLogedOut;
    public Action OnUserSignedIn;
    public Action OnLooseHeart;
    public Action OnGameOver;
    public Action OnBulletNumberChanges;
    public Action OnGameStarted;


    public PanelManager panelManager;
    public PanelManager menuPanel;
    public PanelManager gamePanel;

    public TextHelper welcomeText;
    public TextHelper scoreText;

    public string HighScore => data.user.user.score.ToString(); 
    public string Score => score.ToString();

    #region singeltone
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        data = SaveSystem.Load<GameData>(SavePath);
    }
    #endregion
    private void Start()
    {
        data.achivements = new Achivements();
        UpdateLeaderBoard();
        SetupWelcomeText();
        OnUserLogedOut += SetupWelcomeText;
        OnUserSignedIn += SetupWelcomeText;
        OnGameOver += GameOver;
    }
    public void SaveNewUser(UserFormat userFormat)
    {
        data.user = userFormat;
        Save();
    }
    public async void UpdateLeaderBoard(Action UpdataLeaderBoardTable = null)
    {
        LeaderBoardList result = await networkManager.GetLeaderBoardData();
        if (result == null || result.leaderBoardData == null) return;
        if (result.leaderBoardData.Length != 0)
        {
            data.leaderBoardList = result;
            UpdataLeaderBoardTable?.Invoke();
            Save();
        }
    }
    public async Task<Response> RegisterUser(WWWForm form)
    {
        Response response = await networkManager.Register(form);
        return response;
    }
    public async Task<Response> LoginUser(WWWForm form)
    {
        Response response = await networkManager.Login(form);
        return response;
    }
    public async void UpdateHighScore()
    {
        WWWForm form = new WWWForm();
        JsonSerialization serialization = new JsonSerialization();
        form.AddField("username", data.user.user.username);
        form.AddField("score", score);
        Response response = await networkManager.UpdateHighScore(form, data.user.token);
        if (response.responseCode == 401)
        {
            User user = serialization.Deserialize<User>(response.json);
            data.user.user = user;
        }
    }
    private void Save()
    {
        SaveSystem.Save<GameData>(data, SavePath);
    }
    public bool isAuthenticated()
    {
        if (data.user == null)
        {
            return false;
        }
        return data.user.token != null;
    }
    public void LogOutUser()
    {
        data.user = null;
        Save();
        OnUserLogedOut?.Invoke();
        menuPanel.SetPanel("MainMenu");
    }
    public void Play()
    {
        if (isAuthenticated())
        {
            StartGame();
        }
        else
        {
            menuPanel.SetPanel("Register");
        }
    }
    private void StartGame()
    {
        panelManager.SetPanel("Game");
        gamePanel.SetPanel("Game");
        worldManager.Play();
        OnGameStarted?.Invoke();
        score = 0;
        health = maxHealth;
    }
    private void SetupWelcomeText()
    {
        string username = isAuthenticated() ? data.user.user.username : "";
        welcomeText.text = "Welcome To Brust Bullon " + username;
    }
    public void GainScore()
    {
        score++;
        UpdateScoreText();
    }
    private void UpdateScoreText() => scoreText.text = score.ToString();
    public void OnBallonHitGround()
    {
        health--;
        if (health == 0)
        {
            OnGameOver?.Invoke();
            return;
        }
        else
        {
            OnLooseHeart?.Invoke();
        }
        
    }
    public void GameOver()
    {
        gamePanel.SetPanel("GameOver");
        worldManager.GameOver();
        SaveScore();
    }
    private void SaveScore()
    {
        if (score > data.user.user.score)
        {
            data.user.user.score = score;
            Save();
            UpdateHighScore();
        }
    }
    public void GainBullet()
    {
        data.achivements.bullets++;
        OnBulletNumberChanges?.Invoke();
    }
    public void ShotBullet()
    {
        data.achivements.bullets--;
        OnBulletNumberChanges?.Invoke();
        score += worldManager.AvailableItemsCount();
        worldManager.Clear();
    }
}
[System.Serializable]
public class GameData
{
    public LeaderBoardList leaderBoardList;
    public UserFormat user;
    public int selectedWorld;
    public Achivements achivements;
}
[System.Serializable]
public class LeaderBoardData
{
    public int score;
    public string username;
}
[System.Serializable]
public class Achivements
{
    public int bullets = 0;
    public void reset()
    {
        bullets = 0;
    }
}
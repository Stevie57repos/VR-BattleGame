using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameManager_BS gameManager;
    public PlayerCharacter Player;

    public GameObject UI_StartMenuGO;
    public GameObject UI_BattleGO;
    public GameObject UI_WonGO;
    public GameObject UI_LossGO;

    public GameObject PlayerHealthPanelGO = null;
    public GameObject PlayerManaPanelGO = null;
    public GameObject EnemyHealthPanelGO = null;
    public GameObject EnemyManaPanelGO = null;

    public Scene currentScene;

    [SerializeField] CharacterRegistry _characterRegistry;
    [SerializeField] GameManagerEventChannelSO battleFinish;

    private void OnEnable()
    {
        battleFinish.GameManagerEvent += UnloadPreviousScene;
    }
    private void OnDisable()
    {
        battleFinish.GameManagerEvent -= UnloadPreviousScene;
    }
    private void Awake()
    {
        gameManager = GetComponent<GameManager_BS>();
    }
    private void Start()
    {
        Player = _characterRegistry.Player.GetComponent<PlayerCharacter>();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("UI_MainMenu", LoadSceneMode.Additive);
        currentScene = SceneManager.GetSceneByName("UI_MainMenu");
    }
    public void UnloadPreviousScene()
    {
        if (currentScene.IsValid())
            SceneManager.UnloadSceneAsync(currentScene);
    }
    public void LoadBattleMenuUI()
    {
        SceneManager.LoadSceneAsync("UI_Battle", LoadSceneMode.Additive);
        EnemyManager enemyManager = gameManager.GetComponent<EnemyManager>();
        if (enemyManager.CurrentEnemyGO != null)
            Destroy(enemyManager.CurrentEnemyGO);
        currentScene = SceneManager.GetSceneByName("UI_Battle");
    }
    public void LoadLossMenuUI()
    {
        UnloadPreviousScene();
        SceneManager.LoadSceneAsync("UI_LossMenu", LoadSceneMode.Additive);        
        currentScene = SceneManager.GetSceneByName("UI_LossMenu");
    }
    public void LoadWinMenuUI()
    {
        SceneManager.LoadSceneAsync("UI_WonMenu", LoadSceneMode.Additive);
        currentScene = SceneManager.GetSceneByName("UI_WonMenu");
    }
}

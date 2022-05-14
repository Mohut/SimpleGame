using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private int timeNeeded;
    private bool gameStarted;
    private float timer;
    private int timerInt;
    private int progressBarProgress;
    private VisualElement root;
    private Button playButton;

    private void Awake()
    {
        root = uiDocument.rootVisualElement;
        playButton = root.Q<Button>("PlayButton");
    }
    
    //ToDo if enough enemies are defeated get extra time on the timer

    void Start()
    {
        gameStarted = false;
        timer = 0;
        timerInt = 0;
        
        playButton.clicked += Play;
        EventManager.KillEnemyEvent += ProgressProgressBar;
        EventManager.GameStartEvent += HideMainMenu;
        EventManager.GameStartEvent += StartTimer;
        EventManager.GameOverEvent += ResetTimer;
        EventManager.GameOverEvent += ResetProgressBar;
        EventManager.GameOverEvent += ShowMainMenu;
        EventManager.GameOverEvent += StopTimer;
    }

    private void Update()
    {
        if (gameStarted)
        {
            timer += Time.deltaTime;
            timerInt = (int)timer;
            root.Q<Label>("Timer").text = timerInt.ToString();
        }
    }

    private void OnDestroy()
    {
        playButton.clicked -= Play;
        EventManager.KillEnemyEvent -= ProgressProgressBar;
        EventManager.GameStartEvent -= HideMainMenu;
        EventManager.GameStartEvent -= StartTimer;
        EventManager.GameOverEvent -= ResetTimer;
        EventManager.GameOverEvent -= ResetProgressBar;
        EventManager.GameOverEvent -= ShowMainMenu;
        EventManager.GameOverEvent -= StopTimer;
    }

    private void Play()
    {
        EventManager.GameStart();
    }

    private void StartTimer()
    {
        gameStarted = true;
    }

    private void StopTimer()
    {
        gameStarted = false;
    }

    private void ShowMainMenu()
    {
        root.Q("MainMenuBackground").style.display = DisplayStyle.Flex;
    }

    private void HideMainMenu()
    {
        root.Q("MainMenuBackground").style.display = DisplayStyle.None;
    }

    private void ProgressProgressBar()
    {
        progressBarProgress += 10;
        root.Q("ProgressBarFull").style.width = new StyleLength(Length.Percent(progressBarProgress));
    }

    private void ResetProgressBar()
    {
        root.Q("ProgressBarFull").style.width = new StyleLength(Length.Percent(0));
    }

    private void ResetTimer()
    {
        timer = 0;
        root.Q<Label>("Timer").text = "0";
    }
}

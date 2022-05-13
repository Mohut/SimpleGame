using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private int progressBarProgress;
    private VisualElement root;
    private Button playButton;

    private void Awake()
    {
        root = uiDocument.rootVisualElement;
        playButton = root.Q<Button>("PlayButton");
    }

    void Start()
    {
        playButton.clicked += Play;
        EventManager.KillEnemyEvent += ProgressProgressBar;
        EventManager.GameOverEvent += ResetProgressBar;
        EventManager.GameOverEvent += ShowMainMenu;
    }

    private void OnDestroy()
    {
        playButton.clicked -= Play;
        EventManager.PillCollectedEvent -= ProgressProgressBar;
        EventManager.GameOverEvent -= ResetProgressBar;
    }

    private void Play()
    {
        root.Q("MainMenuBackground").style.display = DisplayStyle.None;
        EventManager.GameStart();
    }

    private void ShowMainMenu()
    {
        root.Q("MainMenuBackground").style.display = DisplayStyle.Flex;
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
}

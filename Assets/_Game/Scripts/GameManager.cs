using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    public Animator transitionImageAnimator;
    public bool isLevelComplete = false;

    private bool isLoading = false;
    private float loadingTimer = 0f;
    private int targetSceneIndex = -1;

    private bool canReturnMenu = true;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        transitionImageAnimator.Play("Out");
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        isLevelComplete = false; // Yeni sahnede sıfırla
        if (transitionImageAnimator)
            transitionImageAnimator.Play("Out");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canReturnMenu)
        {
            canReturnMenu = false;
            transitionImageAnimator.Play("In");
            DOVirtual.DelayedCall(0.5f, delegate
            {
                canReturnMenu = true;
                SceneManager.LoadScene(0);
            });
        }
        if (Input.GetKeyDown(KeyCode.R) && !isLoading)
        {
            RestartLevel();
        }

        if (isLoading)
        {
            loadingTimer += Time.deltaTime;
            if (loadingTimer > 0.5f)
            {
                SceneManager.LoadScene(targetSceneIndex);
                ResetLoadingState();
            }
        }
    }

    public void CompleteLevel()
    {
        if (isLevelComplete || isLoading) return;

        isLevelComplete = true;
        NextLevel();
    }

    public void RestartLevel()
    {
        if (isLoading) return;

        targetSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartLoading();
    }

    public void NextLevel()
    {
        if (isLoading) return;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            targetSceneIndex = currentIndex + 1;
            StartLoading();
        }
        else
        {
            Debug.Log("Son level! Yeni bir sahne kalmadı.");
        }
    }

    private void StartLoading()
    {
        isLoading = true;
        loadingTimer = 0f;
        transitionImageAnimator.Play("In");
    }

    private void ResetLoadingState()
    {
        isLoading = false;
        loadingTimer = 0f;
        targetSceneIndex = -1;
    }
}

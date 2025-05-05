using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] buttons;
    private bool canStart = true;
    public Animator transitionImageAnimator;
    public Text creditText;
    void Start()
    {
        transitionImageAnimator.Play("Out");
        buttons[0].onClick.AddListener(delegate
        {
            if (!canStart) return;
            canStart = false;
            transitionImageAnimator.Play("In");
            DOVirtual.DelayedCall(0.5f, delegate
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });
        });
        buttons[1].onClick.AddListener(delegate
        {
            Application.Quit();
        });
        buttons[2].onClick.AddListener(delegate
        {
            creditText.gameObject.SetActive(!creditText.gameObject.activeSelf);
        });
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishUI : MonoBehaviour
{
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _winUI;

    private void OnEnable()
    {
        Lose.OnLoseUI += OpenLoseUI;
        Cube.OnMaxCubeValueReached += OpenWinUI;
    }

    private void OnDisable()
    {
        Lose.OnLoseUI -= OpenLoseUI;
        Cube.OnMaxCubeValueReached -= OpenWinUI;
    }

    private void OpenLoseUI()
    {
        Time.timeScale = 0f;
        _loseUI.SetActive(true);
    }

    private void OpenWinUI()
    {
        Time.timeScale = 0f;
        _winUI.SetActive(true);
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

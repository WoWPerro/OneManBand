using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject creditPanel;

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    public void CreditScreen() {
        creditPanel.SetActive(true);
    }
    
    public void QuitCreditScreen() {
        creditPanel.SetActive(false);
    }
}

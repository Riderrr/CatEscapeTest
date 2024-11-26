using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.UI
{
    public class CTA : MonoBehaviour
    {
        public CreativeSettings creativeSettings;
        public GameObject ctaGo;
        public GameObject winGo;
        public GameObject loseGo;
        
        public void ShowCTA(bool isWin)
        {
            winGo.SetActive(isWin);
            loseGo.SetActive(!isWin);
            ctaGo.SetActive(true);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OpenURL()
        {
            Application.OpenURL(creativeSettings.playableLink);
        }
    }
}
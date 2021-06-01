using UnityEngine.SceneManagement;

namespace EnglishKids.Robots {
    public class MainMenu : Singleton<MainMenu> {
        public void PlayRobots() {
            SceneManager.LoadScene(1);
        }
    }
}
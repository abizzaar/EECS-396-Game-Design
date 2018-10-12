using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class PauseMenu : Menu
        {
            
            public PauseMenu () {
                Go = (GameObject) Object.Instantiate(Resources.Load("Menus/Pause Menu"), Canvas);
                InitializeButtons();
            }

            /// <summary>
            /// Add listeners to the Pause Menu buttons
            /// </summary>
            private void InitializeButtons () {
                Button resumeButton = Go.transform.Find("Buttons/Resume").GetComponent<Button>();
                Button mainMenuButton = Go.transform.Find("Buttons/Main Menu").GetComponent<Button>();
                resumeButton.onClick.AddListener (Game.Ctx.Clock.Unpause);
                mainMenuButton.onClick.AddListener (MainMenuFunction);
            }

            private void MainMenuFunction()
            {              
                Game.Ctx.ReturnToMenu();
                Hide();
            }
        }
    }

}
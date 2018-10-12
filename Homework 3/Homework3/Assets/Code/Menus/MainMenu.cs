using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class MainMenu : Menu
        {

            public MainMenu () {
				Go = (GameObject) Object.Instantiate(Resources.Load("Menus/Main Menu"), Canvas);
                InitializeButtons();
            }

            /// <summary>
            /// Add listeners to the MainMenu buttons
            /// </summary>
            private void InitializeButtons () {
				Button startButton = Go.transform.Find("Buttons/Start").GetComponent<Button>();
				Button quitButton = Go.transform.Find("Buttons/Quit").GetComponent<Button>();
				startButton.onClick.AddListener (Game.Ctx.StartGame);
				quitButton.onClick.AddListener (Game.Ctx.QuitGame);
            }
        }
    }
}
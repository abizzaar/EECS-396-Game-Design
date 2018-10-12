using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class MainMenu : Menu
        {
			private Button Quit;
			private Button Start;

            public MainMenu () {
                // TODO fill me in
                Go = (GameObject) Object.Instantiate(Resources.Load("Menus/Main Menu"), Canvas);
                Quit = GameObject.Find("Quit").GetComponent<Button>();
                Start = GameObject.Find("Start").GetComponent<Button>();
                InitializeButtons();

            }

            /// <summary>
            /// Add listeners to the MainMenu buttons
            /// </summary>
            private void InitializeButtons () {
                // TODO fill me in
                Quit.onClick.AddListener (Game.Ctx.QuitGame);
                Start.onClick.AddListener(Game.Ctx.StartGame);
            }
        }
    }
}
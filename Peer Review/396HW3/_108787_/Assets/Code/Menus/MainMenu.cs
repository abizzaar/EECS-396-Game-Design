using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class MainMenu : Menu
        {
            private Object main_menu;
            private Button _start;
            private Button _quit;

            public MainMenu ()
            {   
                main_menu = Resources.Load("Menus/Main Menu");
                Go = Object.Instantiate(main_menu) as GameObject;
                Go.transform.SetParent(Canvas);
                Go.transform.position = Canvas.position;
                _start = GameObject.Find("Start").GetComponent<Button>();
                _quit = GameObject.Find("Quit").GetComponent<Button>();
                InitializeButtons();
            }

            /// <summary>
            /// Add listeners to the MainMenu buttons
            /// </summary>
            private void InitializeButtons () {
                _start.onClick.AddListener(Game.Ctx.StartGame);
                _quit.onClick.AddListener(Game.Ctx.QuitGame);
            }
        }
    }
}
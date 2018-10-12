using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class PauseMenu : Menu
        {
            private Object pause_menu;
            private Button _resume;
            private Button _mainmenu;

            public PauseMenu () {
                pause_menu = Resources.Load("Menus/Pause Menu");
                Go = Object.Instantiate(pause_menu) as GameObject;
                Go.transform.SetParent(Canvas);
                Go.transform.position = Canvas.position;
                _resume = GameObject.Find("Resume").GetComponent<Button>();
                _mainmenu = GameObject.Find("Main Menu").GetComponent<Button>();
                InitializeButtons();
            }

            /// <summary>
            /// Add listeners to the Pause Menu buttons
            /// </summary>
            private void InitializeButtons () {
                _resume.onClick.AddListener(Game.Ctx.Clock.Unpause);
                _mainmenu.onClick.AddListener(Game.Ctx.UI.Unpause);
                _mainmenu.onClick.AddListener(Game.Ctx.ReturnToMenu);
            }
        }
    }

}
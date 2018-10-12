using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class PauseMenu : Menu
        {
            private Button Resume;
            private Button Main_Menu;
            public PauseMenu () {
               // TODO fill me in
                Go=(GameObject)Object.Instantiate(Resources.Load("Menus/Pause Menu"), Canvas);
                Resume = GameObject.Find("Resume").GetComponent<Button>();
                Main_Menu = GameObject.Find("Main Menu").GetComponent<Button>();

                InitializeButtons();
            }

            /// <summary>
            /// Add listeners to the Pause Menu buttons
            /// </summary>
            private void InitializeButtons () {
				// TODO fill me in
                Resume.onClick.AddListener(Game.Ctx.Clock.Unpause);
                Main_Menu.onClick.AddListener(Pause_reMenu);
            }
            public void Pause_reMenu(){
                this.Hide();
                Game.Ctx.ReturnToMenu();
            }
        }
    }

}
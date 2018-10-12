using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class PauseMenu : Menu
        {
            public PauseMenu()
            {
                // DONE
                Go = (GameObject)Object.Instantiate(Resources.Load("Menus/Pause Menu"),Canvas.transform);
                //Go.transform.SetParent(Canvas, false);
                InitializeButtons();
            }

            /// <summary>
            /// Add listeners to the Pause Menu buttons
            /// </summary>
            private void InitializeButtons()
            {
                // DONE
                Button[] All_Pause_Button = Go.GetComponentsInChildren<Button>();
                for (int i = 0; i < All_Pause_Button.Length; i++)
                {
                    if (All_Pause_Button[i].name == "Resume")
                    {
                        Button resume = All_Pause_Button[i];
                        resume.onClick.AddListener(Game.Ctx.Clock.Unpause);
                    }

                    if (All_Pause_Button[i].name == "Main Menu")
                    {
                        Button main_menu = All_Pause_Button[i];
                        main_menu.onClick.AddListener(BackToMain);
                    }
                }
            }

            private void BackToMain()
            {
                Game.Ctx.ReturnToMenu();
                Game.Ctx.UI._pause.Hide();
            }
        }
    }

}
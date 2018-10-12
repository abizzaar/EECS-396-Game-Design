using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class MainMenu : Menu
        {
            public MainMenu () {
                // DONE
                Go = (GameObject)Object.Instantiate(Resources.Load("Menus/Main Menu"),Canvas.transform);
                //Go.transform.SetParent(Canvas, false);
                InitializeButtons();
            }

            /// <summary>
            /// Add listeners to the MainMenu buttons
            /// </summary>
            private void InitializeButtons () {
                // DONE
                Button[] All_Button = Go.GetComponentsInChildren<Button>();
                for (int i = 0; i < All_Button.Length; i++)
                {
                    if(All_Button[i].name == "Start"){
                        Button start = All_Button[i];
                        start.onClick.AddListener(Game.Ctx.StartGame);
                    }

                    if (All_Button[i].name == "Quit"){
                        Button quit = All_Button[i];
                        quit.onClick.AddListener(Game.Ctx.QuitGame);
                    }
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
	public partial class UIManager
	{
		private class UpgradeMenu : Menu
		{

			//public static GameObject Go;

			public UpgradeMenu()
			{
				Go = (GameObject)Object.Instantiate(Resources.Load("Upgrade Menu"),Canvas);
				InitializeButtons();
			}

			private void InitializeButtons()
			{
				var _upgrade = GameObject.Find("Upgrade").GetComponent<Button>();
				_upgrade.onClick.AddListener(() =>
				{
					var cell = GameObject.FindGameObjectWithTag("Clicked");
					cell.GetComponent<Renderer>().material.color = Color.gray;
					cell.tag = "Cube";
					GameObject[] cubes = GameObject.FindGameObjectsWithTag("Disabled");
					foreach (var cub in cubes)
					{
						cub.tag = "Cube";
					}
					Hide();	
				});
				
				var _sell = GameObject.Find("Sell").GetComponent<Button>();
				_sell.onClick.AddListener(() =>
				{
					towerBuild[] towers = GameObject.FindObjectsOfType<towerBuild>();
					var cell = GameObject.FindGameObjectWithTag("Clicked");
					Vector3 towerPos = new Vector3(cell.transform.position.x,cell.transform.position.y+2,cell.transform.position.z);
					foreach (towerBuild tower in towers)
					{
						if (tower.transform.position == towerPos)
						{
							tower.gameObject.SetActive(false);
						}	
					}
					Base.dollar = Base.dollar + 0.9f*Base.towerprice;
					cell.layer = 8;
					cell.GetComponent<Renderer>().material.color = Color.gray;
					cell.tag = "Cube";
					GameObject[] cubes = GameObject.FindGameObjectsWithTag("Disabled");
					foreach (var cub in cubes)
					{
						cub.tag = "Cube";
					}
					Hide();	
				});

			}

		}	
	}
}

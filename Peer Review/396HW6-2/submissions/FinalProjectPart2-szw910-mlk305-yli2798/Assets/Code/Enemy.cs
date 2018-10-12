using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : MonoBehaviour
{
	//public GameObject ENEMY;
	private	Vector3 pos;
	private Vector3 Basepos;
	//public GameObject _enemy;
	public int enemyHealth;

	public int GetHealth()
	{
		return enemyHealth;
	} 
	public void SetHealth(int val)
	{
		enemyHealth = enemyHealth - val;
	}
	
	// Use this for initialization
	void Start () {		
		//_enemy = (GameObject) Instantiate(ENEMY);
		//_enemy.GetComponent<Renderer> ().material.color = Color.blue;
		//GetComponent<Renderer> ().material.color = Color.blue;
		//enemyHealth = 100;
		//pos = _enemy.transform.position;
		pos = transform.position;
		//Debug.Log(pos);
		//Debug.Log(enemyHealth);		


	}
	
	// Update is called once per frame
	void Update () {
		if (Base.end)
		{
			return;
		}
		
		GameObject _Base = GameObject.FindWithTag("Base");
		Basepos = _Base.transform.position;
		//Debug.Log(_enemy.transform.position);
		GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
		GameObject[] clicked= GameObject.FindGameObjectsWithTag("Clicked");
		GameObject[] disable= GameObject.FindGameObjectsWithTag("Disabled");
		List<GameObject> towers = new List<GameObject>();
		foreach (var cell in cubes)
		{
			if (cell.layer == 9)
			{
				towers.Add(cell);
				//Debug.Log(cell.transform.position);
			}
		}
		foreach (var cell in clicked)
		{
			if (cell.layer == 9)
			{
				towers.Add(cell);
				//Debug.Log(cell.transform.position);
			}
		}
		foreach (var cell in disable)
		{
			if (cell.layer == 9)
			{
				towers.Add(cell);
				////Debug.Log(cell.transform.position);
			}
		}
		Vector3 dir = calculateDirection(pos, Basepos, towers);
		//Debug.Log(dir);

		if (GetComponent<Renderer>().material.color == Color.cyan)
		{
			pos = pos + Vector3.Normalize(dir) * 0.04f*(EnemyManager.WaveNumber+5);
		}
		else
		{
			pos = pos + Vector3.Normalize(dir) * 0.02f*(EnemyManager.WaveNumber+5);
		}
		
		if (pos.x<=Basepos.x+2.5 && pos.x>=Basepos.x-2.5 && pos.z<=Basepos.z+2.5 && pos.z>=Basepos.z-2.5)
		{
			
			//_enemy.SetActive(false);
			gameObject.SetActive(false);
			if (Base.hit > 0)
			{
				Base.hit = Base.hit - 5;
			}
			
			//Debug.Log(Base.hit);
			//_Base.GetComponent<Base>().hit = _Base.GetComponent<Base>().hit - 10;
			//Debug.Log(_Base.GetComponent<Base>().hit);
		}
		else
		{
			//_enemy.transform.position = pos;
			transform.position = pos;
			//Debug.Log(pos);
		}
		if (enemyHealth <= 0)
		{
			gameObject.SetActive(false);
		}
		

	}

	Vector3 calculateDirection(Vector3 startPos, Vector3 endPos, List<GameObject> towers)
	{
		//keep track of visited positions
		HashSet<Vector3> visited = new HashSet<Vector3>();
		//dict that maps position to parent position
		Dictionary<Vector3, Vector3> dict = new Dictionary<Vector3, Vector3>();
		//queue of next positions
		Queue<Vector3> q = new Queue<Vector3>();
		//add start position to q and visited
		q.Enqueue(startPos);
		visited.Add(startPos);
		float[] direction = {0,1f, 0, -1f, 0};
		while (q.Count > 0)
		{
			Vector3 curr = q.Dequeue();
			//if dequeue a position that's within the range of end cell, path found
			if (curr.x <= endPos.x + 2.5 && curr.x >= endPos.x - 2.5 && 
			    curr.z <= endPos.z + 2.5 && curr.z >= endPos.z - 2.5)
			{
				//Debug.Log("found");
				Vector3 prev=new Vector3();
				dict.TryGetValue(curr, out prev);
				while (prev != startPos)
				{
					if (dict.ContainsKey(prev))
					{
						curr = prev;
						dict.TryGetValue(curr, out prev);
					}
					else
					{
						//Debug.Log("ERROR");
						return new Vector3(0,0,0);
					}
				}
				//Debug.Log(temp-startPos);
				return curr - startPos;
			}
			//add next position if next position is not found in visited and next position is not within range of towers
			for (int i = 0; i < direction.Length - 1; i++)
			{
				Vector3 next=new Vector3(curr.x+direction[i],curr.y,curr.z+direction[i+1]);
				if (next.x < -3 || next.x > 22 || next.y < -3 || next.y > 22)
				{
					//Debug.Log("out of bound");
					continue;
				}
				if (!visited.Contains(next))
				{
					visited.Add(next);
					bool blocked = false;
					foreach (GameObject tower in towers)
					{
						//check if this position has a tower 
						if (next.x < tower.transform.position.x + 2.5 && next.x > tower.transform.position.x - 2.5 &&
						    next.z < tower.transform.position.z + 2.5 && next.z > tower.transform.position.z - 2.5)
						{
							blocked = true;
							break;
						}
					}
					if (!blocked)
					{
			
						dict.Add(next,curr);
						//Debug.Log(next);
						//Debug.Log(curr);
						q.Enqueue(next);
					}
				}
			}
		}
		//Debug.Log("error");
		return new Vector3(0,0,0);
		//iterating through the dict to find the postion where parent position is start pos
		//return difference 
	}


}



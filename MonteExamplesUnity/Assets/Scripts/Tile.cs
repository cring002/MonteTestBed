﻿using UnityEngine;
using System.Collections;

//Class for the tile
public class Tile : MonoBehaviour {

	public Renderer rend;
	private Game master;
	public bool canPress;
	public float realX;
	public float realY;
	public int x;
	public int y;
	public GameObject preFabCounter0;
	public GameObject preFabCounter1;
	private GameObject counter0;
	private GameObject counter1;

	public Texture tileTexture;

	// Use this for initialization
	void Start () {
		rend.material.mainTexture =  tileTexture;
		//init the counters
		counter0 = (GameObject)Instantiate(preFabCounter0, new Vector3 (realX, realY,0 ), Quaternion.Euler(new Vector3(90, 0, 0)));
		counter1 = (GameObject)Instantiate(preFabCounter1, new Vector3 (realX, realY,0 ), Quaternion.Euler(new Vector3(90, 0, 0)));
	}

	//When you mouse over 
	void OnMouseOver()
	{
		rend.material.mainTexture =  tileTexture;
		//If it can be pressed (therefore an empty space)
		if (canPress) {
			//Set the colour to green
			rend.material.color = Color.green;
			if(counter0 != null)counter0.SetActive (false);
			if(counter1 != null)counter1.SetActive (false);
			//And set the counter to active
			if (master.getPlayerColour() == 0 && counter0 != null) counter0.SetActive (true);
			else if (counter1 != null) counter1.SetActive (true);
		}
	}

	void OnMouseDown()
	{
		//If it is the players turn and they can click this space
		if (master.currentPlayersTurn == master.playerIndx && canPress && master.gamePlaying) {
			//Turn the coutner on for good
			if (master.getPlayerColour() == 0) counter0.SetActive (true);
			else counter1.SetActive (true);
			canPress = false;
			//And update the board
			master.handlePlayerAt (x, y);
		}
	}

	void OnMouseExit()
	{
		//When we mouse off the tile we reset the highligh and hide the counter
		rend.material.mainTexture =  tileTexture;
		rend.material.color = Color.white;
		if (canPress) {
			counter0.SetActive (false);
			counter1.SetActive (false);
		}
	}

	public void setMaster(Game _master)
	{
		//Sets which game master we are using.
		master = _master;
	}

	public void setRealXY(float _rX, float _rY)
	{
		//Sets the real world XY location of the tile
		realX = _rX;
		realY = _rY;
	}

	public void setXY(int _x, int _y)
	{
		//Sets the XY board values of the tile
		x = _x;
		y = _y;
	}

	public void playHere(int pieceIndx)
	{
		if (!canPress)return;
		//Function to play the AI move here
		if (pieceIndx == 2) counter0.SetActive (true);
		else counter1.SetActive (true);
		canPress = false;
	}
}

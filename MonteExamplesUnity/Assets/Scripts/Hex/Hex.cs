﻿using System;
using Monte;
using UnityEngine;
using System.Collections.Generic;

public class Hex : Game
{
    public Hex()
    {
        latestAIState = new HexAIState();
    }

	// Play is called once per tick
	public override int checkAI () {
		//If the game is running and it is time for the AI to play
		if (!currentAI.started)
		{
			AIState currentState = new HexAIState((currentPlayersTurn+1)%2, null, 0, latestStateRep, numbMovesPlayed);
			currentAI.run(currentState);
		}
		else if (currentAI.done)
		{
			HexAIState nextAIState = (HexAIState)currentAI.next;
			if (nextAIState == null) reset();
			else
			{
				latestAIState = nextAIState;
				currentAI.reset();
				currentPlayersTurn = (currentPlayersTurn + 1) % 2;
				updateBoard ();
			}
			numbMovesPlayed++;
		}
		if (numbMovesPlayed == 81) return 2;

		return latestAIState.getWinner();
	}

    // Play is called once per tick
    public override int checkSimulation () {
        //If the game is running and it is time for the AI to play
        if (!currentAI.started)
        {
            AIState currentState = new HexAIState((currentPlayersTurn+1)%2, null, 0, latestAIState.stateRep, numbMovesPlayed);
            currentAI.run(currentState);
        }
        else if (currentAI.done)
        {
            HexAIState nextAIState = (HexAIState)currentAI.next;
            if (nextAIState == null)reset();
            else
            {
                latestAIState = nextAIState;
                currentAI.reset();
                currentPlayersTurn = (currentPlayersTurn + 1) % 2;
                currentAI = ais[currentPlayersTurn];
            }
            numbMovesPlayed++;
        }
        if (numbMovesPlayed == 81) return 2;
		return latestAIState.getWinner();
    }

    public override void reset()
    {
        latestAIState = new HexAIState();
        numbMovesPlayed = 0;
        currentPlayersTurn = 0;
    }

	public override void initBoard()
	{
		latestStateRep = new int[81];
		//Creates the new tiles
		board = new List<GameObject>();
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				//create the game object 
				float x = (i+(0.1f*i))-((float)j/2);
				float y = j + (0.1f * j);
				GameObject tile = (GameObject)Instantiate(preFabTile, new Vector3 (x, y, 0), Quaternion.identity);
				tile.GetComponent<Tile>().setMaster(this);
				tile.GetComponent<Tile> ().setXY (i, j);
				tile.GetComponent<Tile> ().setRealXY (x, y);
				board.Add(tile);
				tile.GetComponent<Tile>().preFabCounter0 = preFabCounter0;
				tile.GetComponent<Tile>().preFabCounter1 = preFabCounter1;
			}
		} 
	}

	public override int getPlayerColour()
	{
		return playerIndx;
	}
}
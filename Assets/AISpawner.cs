/*! 
@author EasyMOBA <easymoba.com>
@lastupdate 24 December 2017
*/

using System.Collections.Generic;
using UnityEngine;

public class AISpawner
{
	public class botNames
	{
		public List<string> list = new List<string>();	
	}

	public static botNames _botNames;

	public static MobileAgent SpawnPlayerBot (string clientPrefab, Callipso.GameSession session)
	{
		MobileAgent created = ServerManager.current.JoinGame (null, session, _botNames.list [Random.Range (0, _botNames.list.Count)], null, true);
        AIAgent ai = created.gameObject.AddComponent<AIAgent>();
        ai.agent = created;
        ai.vision = 20;
        created.user = null;

        return created;
    }

    public static MobileAgent SpawnCreature(string clientPrefab, Callipso.GameSession session, ushort teamId)
    {
        Callipso.Hero creature = ServerManager.creatureHeroes.Find(x => x.clientPrefab == clientPrefab);
        MobileAgent created = ServerManager.current.JoinGame(null, session, creature.alias, clientPrefab);
        //created.team = teamId;
        AIAgent ai = created.gameObject.AddComponent<AIAgent>();
        ai.agent = created;
        ai.vision = creature.vision; // Bots always can see
        created.user = null;

        return created;
    }

    public static MobileAgent SpawnBuilding(Map.BuildingInfo building, Callipso.GameSession session)
    {
        Callipso.Hero buildingHero = ServerManager.buildingHeroes.Find(x => x.clientPrefab == building.GetBuildingPrefab());
        MobileAgent created = ServerManager.current.JoinGame(null, session, buildingHero.alias, building.GetBuildingPrefab(), true, buildingInfo: building);
        
        created.team = building.team;
        created.user = null;
        
        AIAgent ai = created.gameObject.AddComponent<AIAgent>();
        ai.agent = created;
        //ai.vision = tower.vision; // Bots always can see
        ai.vision = 20;

        return created;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public string nodeID;
	public List<NodeConnection> connectedNodes;

	public List<string> chestIDs;
}


public struct NodeConnection
{
	List<LogicStates> BypassOptions;
	string nodeIDToConnectTo;
}

[System.Flags]
public enum LogicStates
{
	Arrow,
	ArrowsIce,
	ArrowsFire,
	ArrowsWind,
	Boomarang,
	BoomarangWind,
	BoomarangFire,
	BoomarangIce,

	PantsIron,
	PantsIce,
	PantsHover,
	PantsFeatherFalling,

	Glider,
	Hookshot,
	FishingRod,
	Swapstone,
	Bellows,
	MegaFist,
	QuakeTotem,
	GlimmerCape,
	StatueStaff,
	Shovel,
	
	
}


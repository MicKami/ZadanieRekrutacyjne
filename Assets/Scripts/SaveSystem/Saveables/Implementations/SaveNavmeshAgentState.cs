using System;
using UnityEngine;
using static JsonSerializedTypes;

public class SaveNavmeshAgentState : ISaveable
{
	[SerializeField] private UnityEngine.AI.NavMeshAgent navMeshAgent;

	public Type Type => typeof(SaveData);

	public void RestoreState(object obj)
	{
		var data = (SaveData)obj;
		navMeshAgent.Warp(data.agentPosition);
		navMeshAgent.velocity = data.agentVelocity;
	}

	public object CaptureState()
	{
		return new SaveData()
		{
			agentPosition = navMeshAgent.nextPosition,
			agentVelocity = navMeshAgent.velocity
		};
	}

	[Serializable]
	private struct SaveData
	{
		public Vector3Json agentPosition;
		public Vector3Json agentVelocity;
	}
}

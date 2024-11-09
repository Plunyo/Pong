using Godot;
using System;

public partial class Global : Node
{
	[Signal] public delegate void ResetSignalEventHandler();

	public static Global Instance { get; private set; }

	public int P1Score = 0;
	public int P2Score = 0;

	public override void _Ready()
	{
		Instance = this;
	}

	public void Reset()
	{
		EmitSignal("ResetSignal");
	}
}

using Godot;
using System;

public partial class Trash : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	private Area2D _triggerArea;
	public override void _Ready()
	{
		_triggerArea = GetNode<Area2D>("TriggerArea");
		_triggerArea.BodyEntered += OnTrashBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	private void OnTrashBodyEntered(Node body){
		if(body is CharacterBody2D player){
				GD.Print("trash is touched");
				QueueFree();
			
		}
		
	}
	
}

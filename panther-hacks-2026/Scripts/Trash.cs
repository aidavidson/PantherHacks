using Godot;
using System;
[GlobalClass]
public partial class Trash : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	private Area2D _area2D;
	public override void _Ready()
	{
		_area2D = GetNode<Area2D>("Area2D");
		_area2D.BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	private void OnBodyEntered(Node body){
		
		if(body is Player){
			if(Player.Instance.objsInBag < Player.Instance.bagCapacity){
				GD.Print("Entered");
				Player.Instance.objsInBag += 1;
				Player.Instance.playerSpeed -= 10f;
				QueueFree();
				GD.Print(Player.Instance.objsInBag);
			}
			
		}
		
	}
	
}

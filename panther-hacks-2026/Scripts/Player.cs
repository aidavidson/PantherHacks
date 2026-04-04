using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public float playerSpeed;
	public int handmadeTimer;
	public int Oxygen;
	public int Health;
	
	public int objsInBag;
	public int fishSaved;
	public const int bagCapacity = 20;
	public const int seaLevel = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerSpeed = 300f;
		Oxygen = 100;
		Health = 100;
		objsInBag = 0;
		fishSaved = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Oxygen > 0 && Health > 0){
			HandleMovement(delta);
			handmadeTimer++;
			handmadeTimer = OxygenManagement(handmadeTimer);
		}else{
			//figure out later
			GD.Print("player is dead");
		}
		
		
	}
	
	// handle player movement
	private void HandleMovement(double delta){
		float Xmovement = 0f;
		float Ymovement = 0f;
		if(Input.IsKeyPressed(Key.W)){
			Ymovement = -1f;
		}else if(Input.IsKeyPressed(Key.S)){
			Ymovement = +1f;
		}else if(Input.IsKeyPressed(Key.A)){
			Xmovement = -1f;
		}else if(Input.IsKeyPressed(Key.D)){
			Xmovement = +1f;
		}
		Vector2 MoveDirection = new Vector2(Xmovement, Ymovement).Normalized();
		Position += MoveDirection * playerSpeed * (float)delta;
	}
	
	// manages the oxygen of the player
	private int OxygenManagement(int timer){
		
		if(Position.Y <= seaLevel){
			Oxygen = 100;
		}
		if((timer % 100) == 0){
			Oxygen--;
			GD.Print(Oxygen);
			return 0;
		}
		return timer;
	}
	private void CheckForObject(){
		
	}

}

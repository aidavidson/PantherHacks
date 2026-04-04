using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// constants
	public int bagCapacity = 20;
	public int seaLevel = 0;
	
	//Texture2D leftTexture = GD.Load<Texture2D>();
	//Texture2D rightTexture = GD.Load<Texture2D>();
	
	
	public int handmadeTimer;
	
	
	//player survival vars
	public int Oxygen;
	public int Health;
	
	
	//variables controlled by outside collisions
	public float playerSpeed;
	public int objsInBag;
	public int fishSaved;
	
	//manipulator of player class for the variables controlled by collisions
	public static Player Instance;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerSpeed = 300f;
		Oxygen = 100;
		Health = 100;
		objsInBag = 0;
		fishSaved = 0;
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Oxygen > 0 && Health > 0){
			HandleMovement(delta);
			handmadeTimer++;
			handmadeTimer = OxygenManagement(handmadeTimer);
			//GD.Print(playerSpeed);
		}else{
			//figure out later
			GD.Print("player is dead");
		}
		
		
	}
	
	
	
	
	
	
	
	
	// handle player movement
	/*
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
	*/
	private void HandleMovement(double delta){
		Vector2 velocity = Vector2.Zero;
		if(Input.IsKeyPressed(Key.W)){
			velocity.Y = -1f;
		}else if(Input.IsKeyPressed(Key.S)){
			velocity.Y = +1f;
		}else if(Input.IsKeyPressed(Key.A)){
			
			velocity.X = -1f;
		}else if(Input.IsKeyPressed(Key.D)){
			
			velocity.X = +1f;
		}
		velocity = velocity.Normalized()*playerSpeed;
		Velocity = velocity;
		MoveAndSlide();
	}
	
	
	// manages the oxygen of the player
	private int OxygenManagement(int timer){
		
		if(Position.Y <= seaLevel){
			//will be tied to the boat soon
			Oxygen = 100;
			objsInBag = 0;
			
		}
		if((timer % 100) == 0){
			Oxygen--;
			//GD.Print(Oxygen);
			return 0;
		}
		return timer;
	}
	

}

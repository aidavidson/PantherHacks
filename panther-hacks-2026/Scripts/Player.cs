using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// constants
	public int bagCapacity = 20;
	public int seaLevel = 0;
	
	private Sprite2D _sprite;
	private Texture2D _leftTexture;
	private Texture2D _rightTexture;
		
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
		_sprite = GetNode<Sprite2D>("Sprite2D");
		_rightTexture = GD.Load<Texture2D>("res://Sprites/Diver-1-Right.png");
		_leftTexture = GD.Load<Texture2D>("res://Sprites/Diver1_big.png");
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
		}else{
			//figure out later
			GD.Print("player is dead");
		}
		
		
	}
	
	
	
	
	
	
	
	
	// handle player movement
	private void HandleMovement(double delta){
		Vector2 velocity = Vector2.Zero;
		if(Input.IsKeyPressed(Key.W)){
			velocity.Y = -1f;
		}else if(Input.IsKeyPressed(Key.S)){
			velocity.Y = +1f;
		}else if(Input.IsKeyPressed(Key.A)){
			_sprite.Texture = _leftTexture;
			velocity.X = -1f;
		}else if(Input.IsKeyPressed(Key.D)){		
			_sprite.Texture = _rightTexture;
			velocity.X = +1f;
		}
		velocity = velocity.Normalized() * playerSpeed;
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

using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// harpoon code
	[Export] public PackedScene projectileScene; // drag your harpoon.tscn here
	public float harpoonSpeed = 500f;    // optional: speed of the harpoon
	private float reloadTimer = 0f;               // internal timer for cooldown
	[Export] public float reloadTime = 0.2f;      // time between shots
	
	
	
	
	// constants
	public int bagCapacity = 20;
	public int seaLevel = 0;
	
	// instance texture and sprite variables for changes
	private Sprite2D _sprite;
	private Texture2D _leftTexture;
	private Texture2D _rightTexture;
	
	// timer and reload code
	public int OxygenTimer;
	public int DamageTimer;
	private float _timer = 0f;
	private float _waitTime = 5.0f; // 5 seconds
	
	
	//player survival vars
	public int Oxygen;
	public int Health;
	
	
	//variables controlled by outside collisions
	public float playerSpeed;
	public int objsInBag;
	public int fishSaved;
	public int totalTrash;
	
	//manipulator of player class for the variables controlled by collisions
	public static Player Instance;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//player is classified as a player
		this.AddToGroup("player");
		Instance = this;
		//sprite node
		_sprite = GetNode<Sprite2D>("Sprite2D");
		//player textures
		_rightTexture = GD.Load<Texture2D>("res://Sprites/Diver-1-Right.png");
		_leftTexture = GD.Load<Texture2D>("res://Sprites/Diver1_big.png");
		//harpoon scene
		projectileScene = GD.Load<PackedScene>("res://Object_Scenes/Harpoon.tscn");
		
		//default player aspects
		playerSpeed = 300f;
		Oxygen = 100;
		Health = 100;
		objsInBag = 0;
		totalTrash = 0;
		fishSaved = 0;
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if(Oxygen > 0 && Health > 0){
			//LevelCompleted();
			HandleMovement(delta);
			OxygenTimer++;
			OxygenTimer = OxygenManagement(OxygenTimer);
			DamageTimer++;
			DamageTimer = EnemyDamageHandling(DamageTimer);
			_timer += (float)delta;
			if (Input.IsMouseButtonPressed(MouseButton.Left) && _timer >= _waitTime){
				GD.Print("Harpoon");
				ShootHarpoon(GetGlobalMousePosition());
				_timer = 0f;
			}
			
		}else{
			//figure out later
			GD.Print("I am not commented");
		}
		
		
		
		
	}
	
	
	private void LevelCompleted(){
		int threshhold = 1;
		Node globalMenu = GetNodeOrNull("/root/StartMenu");
		if(globalMenu == null){
				GD.Print("null");
		}
		int Level = (int)globalMenu.Get("Level");
		if(totalTrash == threshhold){
			
			GD.Print("hello");
			
			bool isLevel2Unlocked = (bool)globalMenu.Get("level2Unlocked");
			if(isLevel2Unlocked == false){
				globalMenu.Set("level2Unlocked", true);
				GD.Print("Is Level 2 unlocked");
				GD.Print(globalMenu.Get("level2Unlocked"));
			}else{
				globalMenu.Set("level3Unlocked", true);
				GD.Print("Is Level 3 unlocked");
				GD.Print(globalMenu.Get("level3Unlocked"));
			}
			GetTree().ChangeSceneToFile("res://Menu/menu.tscn");
			//test comment for github
		}
	}
	
	
	
	
	
	// handle player movement
	private void HandleMovement(double delta){
		Vector2 velocity = Vector2.Zero;
		if(Input.IsKeyPressed(Key.W)){
			velocity.Y = -1f;
		}else if(Input.IsKeyPressed(Key.S)){
			velocity.Y = +1f;
		}
		if(Input.IsKeyPressed(Key.A)){
			_sprite.Texture = _leftTexture;
			velocity.X = -1f;
		}else if(Input.IsKeyPressed(Key.D)){		
			_sprite.Texture = _rightTexture;
			velocity.X = +1f;
		}
		velocity = velocity.Normalized() * playerSpeed;
		Velocity = velocity;
		MoveAndSlide();
		if(Position.Y <= 0){
			Vector2 pos = Position;
			pos.Y = 1;
			Position = pos;
		}
	}
	
	
	
	
	
	
	
	
	
	// manages the oxygen of the player
	private int OxygenManagement(int timer){
		ResetValues();
		if((timer % 100) == 0){
			Oxygen--;
			//GD.Print(Oxygen);
			return 0;
		}
		return timer;
	}
	
	private int EnemyDamageHandling(int timer){
		for(int i = 0; i < GetSlideCollisionCount(); i++){
			KinematicCollision2D collision = GetSlideCollision(i);
			Node collider = (Node)collision.GetCollider();
			if(collider.IsInGroup("Enemy")){
				if((timer % 100) == 0){
					Health -= 5;
					return 0;
				}
			}
		}
		return timer;
	}
	
	private void ResetValues(){
		for(int i = 0; i < GetSlideCollisionCount(); i++){
			KinematicCollision2D collision = GetSlideCollision(i);
			Node collider = (Node)collision.GetCollider();
			if(collider.IsInGroup("boat")){
				Oxygen = 100;
				objsInBag = 0;
			}
		}
	}


	private void ShootHarpoon(Vector2 MousePosition){
		Vector2 Direction = (MousePosition-GlobalPosition).Normalized();
		var projectile = (RigidBody2D)projectileScene.Instantiate();
		projectile.Position = GlobalPosition;
		projectile.Rotation = Direction.Angle();
		float speed = 500f;
		projectile.LinearVelocity = Direction * speed;
		GetParent().AddChild(projectile);
		//hello
	}
	
	
	
	
	
	

}

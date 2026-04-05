extends CharacterBody2D

@export var speed: float = 200.0
@export var max_health: int = 100

var health: int = max_health

func _ready():
	add_to_group("player")

func _physics_process(_delta):
	var input = Vector2.ZERO
	
	if Input.is_action_pressed("ui_right"):
		input.x += 1
	if Input.is_action_pressed("ui_left"):
		input.x -= 1
	if Input.is_action_pressed("ui_up"):
		input.y -= 1
	if Input.is_action_pressed("ui_down"):
		input.y += 1
	
	velocity = input.normalized() * speed
	move_and_slide()

func take_damage(amount: int):
	health -= amount
	print("Player hit! Health: ", health)
	if health <= 0:
		print("Player died!")
		health = max_health  # just respawns for testing instead of game over

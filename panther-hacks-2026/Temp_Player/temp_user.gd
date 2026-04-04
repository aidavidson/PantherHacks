extends CharacterBody2D

@export var speed = 300

func _physics_process(delta:):
	
	if Input.is_action_pressed("move_right"):
		velocity.x = speed
	
	if Input.is_action_pressed("move_left"):
		velocity.x = -speed
		
	if Input.is_action_pressed("up"):
		velocity.y = -speed
		
	if Input.is_action_pressed("down"):
		velocity.y = speed
		
	move_and_slide()

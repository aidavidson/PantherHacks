extends CharacterBody2D

@export var speed: float = 100.0
@export var max_speed: float = 250.0
@export var damage: int = 10

var moving_right: bool = true
var start_x: float
var chasing: bool = false
var player = null
var damage_timer: float = 0.0
var chase_timer: float = 0.0  # tracks how long we've been chasing

func _ready():
	start_x = global_position.x
	$DetectionArea.body_entered.connect(_on_body_entered)
	$DetectionArea.body_exited.connect(_on_body_exited)
	$HitboxArea.body_entered.connect(_on_hit)

func _physics_process(delta):
	damage_timer -= delta
	if damage_timer < 0:
		damage_timer = 0

	if chasing:
		# safety check in case player gets deleted
		if not is_instance_valid(player):
			chasing = false
			player = null
		else:
			# the longer we chase, the faster we get, up to max_speed
			chase_timer += delta
			var current_speed = min(speed + chase_timer * 20.0, max_speed)

			if player.global_position.x > global_position.x:
				velocity.x = current_speed
			else:
				velocity.x = -current_speed
			if player.global_position.y > global_position.y:
				velocity.y = current_speed
			else:
				velocity.y = -current_speed
	else:
		velocity.y = 0
		if moving_right:
			velocity.x = speed
		else:
			velocity.x = -speed
		if global_position.x > start_x + 200:
			moving_right = false
		if global_position.x < start_x - 200:
			moving_right = true

	if velocity.x < 0:
		$Sprite2D.flip_h = true
	else:
		$Sprite2D.flip_h = false

	move_and_slide()

func _on_body_entered(body):
	if body.is_in_group("player"):
		chasing = true
		player = body
		chase_timer = 0.0  # reset speed ramp when chase starts

func _on_body_exited(body):
	if body.is_in_group("player"):
		chasing = false
		player = null
		# head back toward start so patrol recenters correctly
		if global_position.x > start_x:
			moving_right = false
		else:
			moving_right = true

func _on_hit(body):
	if body.is_in_group("player"):
		if damage_timer <= 0:
			body.take_damage(damage)
			damage_timer = 1.0

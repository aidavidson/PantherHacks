extends CharacterBody2D

@export var speed: float = 200.0
@export var max_speed: float = 300.0
@export var damage: int = 10
var moving_right: bool = true
var start_x: float
var chasing: bool = false
var player = null
var damage_timer: float = 0.0
var chase_timer: float = 0.0

func _ready():
	add_to_group("Enemy")
	start_x = global_position.x
	$DetectionArea.body_entered.connect(_on_body_entered)
	$DetectionArea.body_exited.connect(_on_body_exited)

func _physics_process(delta):
	damage_timer = max(damage_timer - delta, 0.0)

	if chasing:
		if not is_instance_valid(player):
			chasing = false
			player = null
		else:
			chase_timer += delta
			var current_speed = min(speed + chase_timer * 20.0, max_speed)
			var direction = (player.global_position - global_position).normalized()
			velocity = direction * current_speed
	else:
		velocity.y = 0
		velocity.x = speed if moving_right else -speed
		if global_position.x > start_x + 200:
			moving_right = false
		elif global_position.x < start_x - 200:
			moving_right = true

	$Sprite2D.flip_h = velocity.x < 0
	move_and_slide()

func die():
	#AudioManager.play("damage_player_grunt")
	queue_free()

func _on_body_entered(body):
	if body.is_in_group("player"):
		chasing = true
		player = body
		chase_timer = 0.0

func _on_body_exited(body):
	if body.is_in_group("player"):
		chasing = false
		player = null
		moving_right = global_position.x <= start_x

extends CharacterBody2D
class_name Fish

@onready var timer := $Timer
@onready var area := $Area2D



func _ready() -> void:
	moveRandomly()

func _physics_process(delta: float) -> void:
	var move = true
	for body in area.get_overlapping_bodies():
		if body is Trash:
			move = false
	if move:
		move_and_slide()
	
func moveRandomly():
	print("move")
	var speed = randf_range(150, 250)
	var angle = randf_range(0, 2*PI)
	velocity[0] = speed*cos(angle)
	velocity[1] = speed*sin(angle)

func _on_timer_timeout() -> void:
	moveRandomly()

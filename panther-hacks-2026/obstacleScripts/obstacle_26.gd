extends AnimatableBody2D

@export var spin_speed = 3.0

func _physics_process(delta):
	rotation += spin_speed * delta

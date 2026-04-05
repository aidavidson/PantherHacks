extends CharacterBody2D
class_name Fish

@onready var timer := $MovementCoolDown
@onready var area := $Area2D
var prevVelocity



func _ready() -> void:
	moveRandomly()

func _physics_process(delta: float) -> void:
	$Sprite2D.flip_h = velocity[0] < 0
	
	var move = true
	for body in area.get_overlapping_bodies():
		if body is Trash:
			move = false
	if move:
		move_and_slide()
		if $CollideBuffer.is_stopped():
			for i in range(get_slide_collision_count()):
				var collideAngle = get_slide_collision(i).get_angle()
				if collideAngle == 0 or (collideAngle <= PI + 0.1 and collideAngle >= PI - 0.1):
					velocity[1] = -1 * prevVelocity[1]
				if (collideAngle <= PI/2 + 0.1 and collideAngle >= PI/2 - 0.1) or (collideAngle <= 3*PI/2 + 0.1 and collideAngle >= 3*PI/2 - 0.1):
					velocity[0] = -1 * prevVelocity[0]
				$CollideBuffer.start()
				
	prevVelocity = velocity
	
func moveRandomly():
	var speed = randf_range(150, 250)
	var angle = randf_range(0, 2*PI)
	velocity[0] = speed*cos(angle)
	velocity[1] = speed*sin(angle)

func _on_timer_timeout() -> void:
	moveRandomly()

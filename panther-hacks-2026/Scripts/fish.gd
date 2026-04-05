extends CharacterBody2D
class_name Fish

<<<<<<< HEAD
@onready var timer := $MovementCoolDown
@onready var area := $Area2D
var prevVelocity
=======
@onready var timer := $Timer
@onready var area := $Area2D
>>>>>>> f1efec44e638adffb340b3389307402c2522ef1d



func _ready() -> void:
	moveRandomly()

<<<<<<< HEAD
func _physics_process(delta: float) -> void:
	$Sprite2D.flip_h = velocity[0] < 0
	
=======
func _physics_process(_delta: float) -> void:
>>>>>>> f1efec44e638adffb340b3389307402c2522ef1d
	var move = true
	for body in area.get_overlapping_bodies():
		if body is Trash:
			move = false
	if move:
		move_and_slide()
<<<<<<< HEAD
		if $CollideBuffer.is_stopped():
			for i in range(get_slide_collision_count()):
				var collideAngle = get_slide_collision(i).get_angle()
				if collideAngle == 0 or (collideAngle <= PI + 0.1 and collideAngle >= PI - 0.1):
					velocity[1] = -1 * prevVelocity[1]
				if (collideAngle <= PI/2 + 0.1 and collideAngle >= PI/2 - 0.1) or (collideAngle <= 3*PI/2 + 0.1 and collideAngle >= 3*PI/2 - 0.1):
					velocity[0] = -1 * prevVelocity[0]
				$CollideBuffer.start()
				
	prevVelocity = velocity
=======
>>>>>>> f1efec44e638adffb340b3389307402c2522ef1d
	
func moveRandomly():
	var speed = randf_range(150, 250)
	var angle = randf_range(0, 2*PI)
	velocity[0] = speed*cos(angle)
	velocity[1] = speed*sin(angle)

func _on_timer_timeout() -> void:
	moveRandomly()

extends RigidBody2D

@export var max_distance: float = 400.0

var start_position: Vector2
var has_hit: bool = false

func _ready():
	add_to_group("Harpoon")
	start_position = global_position
	# Connect body entered signal for collision detection
	body_entered.connect(_on_body_entered)

func _physics_process(delta):
	# Delete harpoon if it has traveled its max distance
	if global_position.distance_to(start_position) >= max_distance:
		queue_free()

func _on_body_entered(body):
	if has_hit:
		return
	has_hit = true
	# Enemy fish handles its own death when harpoon enters its HitboxArea
	# This just deletes the harpoon on hitting anything solid
	queue_free()

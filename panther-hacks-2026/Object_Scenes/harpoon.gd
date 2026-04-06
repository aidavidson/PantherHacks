extends RigidBody2D

@export var max_distance: float = 400.0
var start_position: Vector2
var has_hit: bool = false

func _ready():
	add_to_group("Harpoon")
	contact_monitor = true
	max_contacts_reported = 4
	var player = get_tree().get_first_node_in_group("player")
	if player:
		add_collision_exception_with(player)
	body_entered.connect(_on_body_entered)
	await get_tree().process_frame
	start_position = global_position

func _physics_process(delta):
	if global_position.distance_to(start_position) >= max_distance:
		queue_free()

func _on_body_entered(body):
	if has_hit:
		return
	if body.is_in_group("player"):
		return
	# Only kill and disappear if it hit an enemy
	if body.is_in_group("Enemy"):
		has_hit = true
		body.die()
		await get_tree().process_frame
		queue_free()

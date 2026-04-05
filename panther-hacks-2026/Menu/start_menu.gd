extends Control

@onready var play := $Play
@onready var levelMenu := $LevelMenu
var level2Unlocked = false
var level3Unlocked = false

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass



func _on_play_pressed() -> void:
	levelMenu.visible = true
	play.visible = false


func _on_level_1_pressed() -> void:
	get_tree().change_scene_to_file("res://Levels/level_1.tscn")


func _on_level_2_pressed() -> void:
	if level2Unlocked:
		get_tree().change_scene_to_file("res://Levels/level_2.tscn")
	else:
		if not $LevelMenu/Level2/Label.visible:
			$LevelMenu/Level2/Label.visible = true
		if $LevelMenu/Level3/Label.visible:
			$LevelMenu/Level3/Label.visible = false


func _on_level_3_pressed() -> void:
	if level3Unlocked:
		get_tree().change_scene_to_file("res://Levels/level_3.tscn")
	else:
		if not $LevelMenu/Level3/Label.visible:
			$LevelMenu/Level3/Label.visible = true
		if $LevelMenu/Level2/Label.visible:
			$LevelMenu/Level2/Label.visible = false

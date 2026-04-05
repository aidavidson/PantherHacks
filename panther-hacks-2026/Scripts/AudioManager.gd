extends Node

#  AudioManager.gd — Scuba Diver Game (Simple Version)
#  Add as Autoload: Project Settings → Autoload → AudioManager
#
#  HOW TO USE FROM ANY OTHER SCRIPT:
#    AudioManager.play("collect_trash")
#    AudioManager.danger_music(true)
#    AudioManager.danger_music(false)

#  make sure the node names in  scene match the names below.

var sounds = {}
var danger_on = false
var damage_timer = 0.0

func _ready():
	# Collect all child AudioStreamPlayer nodes into a dictionary
	for child in get_children():
		sounds[child.name] = child

	# Start looping ambient sounds
	sounds["AmbientOcean"].play()
	sounds["AmbientBubbles"].play()
	sounds["BoatEngine"].play()

	# Start both music tracks — danger starts silent
	sounds["MusicExplore"].play()
	sounds["MusicDanger"].play()
	sounds["MusicDanger"].volume_db = -80.0

func _process(delta):
	# Count down the damage sound cooldown
	if damage_timer > 0.0:
		damage_timer -= delta

#  PLAY A SOUND — call AudioManager.play("sound_name")
func play(sound_name):
	if sound_name == "SFXDamage":
		# Don't play damage sound too rapidly
		if damage_timer > 0.0:
			return
		damage_timer = 0.5

	if sounds.has(sound_name):
		sounds[sound_name].play()
	else:
		print("AudioManager: sound not found — ", sound_name)

#  DANGER MUSIC — call AudioManager.danger_music(true/false)
func danger_music(active):
	# Don't do anything if already in that state
	if active == danger_on:
		return
	danger_on = active

	var tween = create_tween()
	tween.set_parallel(true)

	if active:
		# Fade danger music IN, exploration music OUT
		sounds["SFXEnemyAggro"].play()
		tween.tween_property(sounds["MusicDanger"],  "volume_db", 0.0,   1.5)
		tween.tween_property(sounds["MusicExplore"], "volume_db", -80.0, 1.5)
	else:
		# Fade exploration music IN, danger music OUT
		tween.tween_property(sounds["MusicExplore"], "volume_db", 0.0,   1.5)
		tween.tween_property(sounds["MusicDanger"],  "volume_db", -80.0, 1.5)

#  BOAT PROXIMITY — call AudioManager.set_boat_proximity(t)
#  t = 0.0 (far away) to 1.0 (right at the boat)
func set_boat_proximity(t):
	sounds["BoatEngine"].volume_db = lerp(-30.0, -8.0, t)

#  VOLUME SLIDERS — wire these to your settings menu
func set_master_volume(db):
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Master"), db)

func set_music_volume(db):
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Music"), db)

func set_sfx_volume(db):
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("SFX"), db)

func set_ambient_volume(db):
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Ambient"), db)

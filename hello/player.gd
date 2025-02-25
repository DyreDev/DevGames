extends CharacterBody2D

# Variáveis de configuração
var speed = 300.0
var jump_speed = -500.0
# Pega a gravidade das configurações do projeto
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

func _physics_process(delta):
	# Adiciona a gravidade apenas se não estiver no chão
	if not is_on_floor():
		velocity.y += gravity * delta

	# Manipula o pulo
	if Input.is_action_just_pressed("ui_up") and is_on_floor():
		$SoundJump.play()
		velocity.y = jump_speed

	# Obtém a direção do input
	var direction = Input.get_axis("ui_left", "ui_right")
	velocity.x = direction * speed

	# Atualiza a animação
	if direction > 0:
		$AnimatedSprite2D.flip_h = false
		$AnimatedSprite2D.play()
	elif direction < 0:
		$AnimatedSprite2D.flip_h = true
		$AnimatedSprite2D.play()
	else:
		$AnimatedSprite2D.stop()

	# Movimenta o personagem
	move_and_slide()

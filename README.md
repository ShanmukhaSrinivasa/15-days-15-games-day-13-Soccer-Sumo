15-Days-15-Games-Day-14-Soccer-Sumo

This is the fourteenth game from my "15 Days 15 Games" challenge. It is a 3D top-down arena game combining physics-based "sumo" mechanics with a soccer-inspired goal defense objective. It features enemy waves, player power-ups, and a boost ability.

🚀 About the Game

The player controls a sphere within an arena containing two goals. Enemy soccer ball spheres spawn in waves and attempt to reach the player's goal. The player must use physics-based collisions, enhanced by temporary power-ups and a cooldown-limited boost ability, to knock the enemy balls into the opposing goal or off the arena edge. Enemies reaching the player's goal reduce player lives. The game continues through escalating waves until the player runs out of lives.

💡 Technical Highlights

Engine: Unity (3D Physics)

Player Boost Ability: The PlayerControllerX implements a forward boost (AddForce with ForceMode.Impulse) triggered by player input. A Coroutine (BoostCoolDown) manages a cooldown timer (canBoost boolean) to prevent ability spamming, accompanied by particle effects (boostParticle.Play()) for visual feedback.

Goal-Oriented Enemy AI: The EnemyX script directs enemies towards a specific playerGoal GameObject using physics forces (Rigidbody.AddForce based on the normalized direction vector).

Unique Scoring/Lives System: The game features a non-traditional scoring mechanism tied to lives. Enemies reaching the Player Goal decrement lives, while enemies being destroyed by hitting the Enemy Goal or falling off the arena increment lives (acting as defensive points).

Refined Wave Spawning: The SpawnManagerX manages wave progression (waveCount), spawns enemies based on the current wave number, ensures periodic power-up spawns, and resets the player's position between waves for a clean start.

Modular Power-Up: A focused power-up system grants temporary enhanced pushing strength (powerupStrength), managed by a Coroutine (PowerupCooldown) and visualized by an indicator object.

▶️ Play the Game!

You can play the game in your browser on its itch.io page:
[Link to your new itch.io page for this game]

# Cristales del Bosque

Prototipo de videojuego 2D de plataformas hecho en Unity para la **Prueba 02: Arquitectura y Lógica de un Videojuego** (Desarrollo de Video Juegos).

**Autor:** Agustín Narciso Inzunza Montecinos
**Repositorio:** https://github.com/Berettla/Ev2-gameCristales

## 1. Objetivo

El jugador controla a un pequeño dinosaurio que recorre una cueva de cristales. Debe **recoger los 5 cristales** del nivel, evitar los **pinchos** y los **pozos**, y llegar a la **salida** para ganar. Si toca un pincho o cae al vacío, pierde y puede reintentar o volver al menú.

Flujo del juego: `MainMenu` → `Level1` → pantalla de victoria o derrota → reintentar o volver al menú.

## 2. Plataforma

**PC (Windows).** Es la plataforma de despliegue trabajada en el curso. El juego se controla solo con teclado y el build es un ejecutable que no requiere instalación.

## 3. Configuración clave

| Elemento | Valor |
|---|---|
| Versión de Unity | 6000.2.3f1 |
| Plantilla / pipeline | Universal 2D (URP, renderer 2D) |
| Cámara | Ortográfica, Size 5, sigue al jugador en `LateUpdate` |
| Física | Gravity Y -9.81, Gravity Scale del jugador 3, Jump Force 10, Rigidbody2D con Interpolate y Freeze Rotation Z |
| Material físico | `NoFriction` (fricción 0) en el jugador, para que no se pegue a las paredes |
| Terreno | Tilemap con `Tilemap Collider 2D` + `Composite Collider 2D`, capa `Ground`, piezas de 128 px (PPU 128) |
| Personaje | Hoja de sprites de 32×32 px (PPU 32), animaciones Idle, Run y Jump |
| Fondo | Dos capas con parallax en espejo (script `MirroredParallax`), sin estirar la imagen |

**Organización de `Assets/`:** `Art` (recursos gráficos), `Tiles` (tiles del Tilemap), `Prefabs` (pincho), `Animations`, `Audio` y `Scripts`. Los parámetros editables (velocidad, fuerza de salto, volúmenes, colores del panel final) son `[SerializeField]` y se ajustan desde el Inspector.

**Scripts principales:** `PlayerMovement` (movimiento y salto), `PlayerAnimation` (animaciones), `CameraFollow`, `MirroredParallax`, `GameManager` (puntuación, victoria, derrota y reinicio), `CrystalCollectible`, `DeathZone` (pinchos y vacío), `ExitGoal`, `AudioManager` y `MainMenu`.

## 4. Instrucciones de ejecución

**Jugar:** abrir `Build/Ev2-gameCristales.exe`. Si Windows muestra el aviso de SmartScreen, elegir *Más información* → *Ejecutar de todos modos*. Para cerrar el juego, usar el botón SALIR del menú o `Alt + F4`.

**Abrir el proyecto:** instalar Unity Hub y la versión **6000.2.3f1**, agregar la carpeta del repositorio como proyecto y abrir la escena `Assets/Scenes/MainMenu.unity`. La primera apertura puede tardar mientras Unity regenera la carpeta `Library`.

## 5. Controles

| Acción | Tecla |
|---|---|
| Moverse | Flechas izquierda/derecha o `A` / `D` |
| Saltar | `Espacio` |
| Reiniciar (pantalla final) | `R` o botón REINTENTAR |

## 6. Dependencias y créditos

Paquetes de Unity: Universal RP, 2D Tilemap y TextMeshPro.

**Gráficos**
- *Free Crystal Caves 2D Platformer Tileset* — CraftPix.net. Uso libre en proyectos ilimitados según su licencia: https://craftpix.net/freebies/free-crystal-caves-2d-platformer-tileset/
- *Lil Dino (Free pixelart animated character)* — Luke Productions. Uso personal y comercial, sin atribución obligatoria: https://lukeproductions.itch.io/lil-dino-free-pixelart-animated-character

**Audio** (OpenGameArt.org)
- Música de fondo: *Crystal Cave + Mysterious Ambience (seamless loop)* — cynicmusic (pixelsphere.org / The Cynic Project), subida por congusbongus. Licencia CC-BY 3.0. https://opengameart.org/content/crystal-cave-mysterious-ambience-seamless-loop
- Efecto de victoria: *Victory* — celestialghost8. Licencia CC0. https://opengameart.org/content/victory
- Efecto de derrota: *This Game Is Over* — Joseph Pueyo (http://www.josephpueyo.com/). Licencia CC-BY-SA 3.0. https://opengameart.org/content/this-game-is-over
- Efecto de cristal recogido: *Item Pickup / Key* — Musheran. Licencia CC0. https://opengameart.org/content/item-pickup-key

## 7. Riesgos y mitigaciones

| Riesgo | Mitigación aplicada |
|---|---|
| El fondo temblaba al mover al jugador | Interpolate en el Rigidbody2D y cámara actualizada en `LateUpdate` |
| La imagen de fondo no se podía repetir ni estirar sin verse mal | Tres copias en espejo con 0,5 % de solape, escalando solo hasta cubrir la altura de la cámara |
| El jugador se quedaba pegado a las paredes | Physics Material 2D sin fricción |
| La animación de salto empezaba tarde | La animación se decide por la velocidad vertical y no por el raycast de suelo |
| Saltos imposibles al diseñar el nivel | Plataformas calculadas con el salto real (~1,7 de alto y ~3,4 de largo) y probadas en el build |
| El `.gitignore` excluía la carpeta `Build` | Se agregó con `git add -f`; ningún archivo supera los 100 MB de GitHub |
| Conflicto al subir cambios | Se integraron los cambios remotos con `git pull` antes de hacer `push` |

## 8. Roles

Proyecto realizado de forma individual por **Agustín Narciso Inzunza Montecinos**: diseño y programación de la lógica, construcción del nivel, integración de arte, animaciones y audio, y compilación.

## 9. Estructura del repositorio

```
Assets/            contenido del juego
Packages/          dependencias de Unity
ProjectSettings/   configuración del proyecto
Build/             compilación para Windows (Ev2-gameCristales.exe)
README.md
```

# HardLand :pushpin:

## Introducción
Este juego fue realizado como **Proyecto** 3D de entrega del trayecto profesional Desarrollo de Videojuegos con Unity del instituto [Image Campus](https://www.imagecampus.edu.ar).

## Game Design

### Concepto general
Es una juego SinglePlayer, primera persona en el que debes juntar Chips dispersos por toda una __zona_ de trampas y obstáculos. Cuentas con tres vidas para superar tres niveles de juego.

Existen trampas tales como Minas, que al pasar por encima de ellas explotan en menos de un segundo. Otros Obstáculos como paredes y esferas que se mueven.

Cada en cada nivel aumenta la cantidad de Chips a juntar y la cantidad de Minas, también reduce el tiempo de explosión de las Minas.

### Plataforma target
El juego fue pensado para plataformas de escritorio desde su inicio.

### Características Especiales
**Detector de minas:** Se dispondrá de una barra de color entre rojo y verde que determina la distancia a la Mina más cerca dentro de un radio.

## Game Development

### Características de Motor
- UI Canvas.
- Animaciones de UI.
- Partículas: explosión de Minas.
- No se utilizó Físicas para movimientos de cualquier objeto.

### Software
- Patrones: 
  - Singleton: un AudioSource que no se destruye entre escenas. 
  - Estado: Estados de una Mina: Reposo, Contacto, Explosión, Detonada.
- Controladores: GameController, CanvasController, PlayerController, SoundController, etc.
- Detector de Minas: __Lerp__ entre dos colores según la Mina más cercana. Se genera una colección de Minas a medida que éstas interactúan con un Collider de radio X.
- Pooling de Minas y Chips.
- Cada vez que se instancia un nivel, los Chips y Minas se ubican aleatoriamente dentro de los límites de la zona de trampas y objetos.

## Prueba el juego y envía feedback
El juego está compilado para plataforma __Windows__.

Para probarlo, debes descargar el repositorio como Zip, descomprimirlo y ejecutar __Build/HardLand.exe__.

## Expectativas
El juego cuenta con poco arte 3D como texturas y objetos. También los Sprites de UI son básicos a modo de prototipo.

Debido a esto, se está a disposición de aceptar trabajo artístico de forma libre para mejorar el juego y agregar créditos.

El juego no tiene fines comerciales, sino aplicar prácticas en el mundo de vodeojuegos, compartir conocimientos y experiencias.

## Contáctame
Puedes enviarme mensajes de correo electrónico a __mmchamoo@gmail.com__ o agregarme a [LinkedIn](https://www.linkedin.com/in/mauricio-manuel-chamorro).

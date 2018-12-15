# HardLand

##Introducción
Este juego fue realizado como **Proyecto** 3D de entrega del trayecto profesional Desarrollo de Videojuegos con Unity del instituto [Image Campus](https://www.imagecampus.edu.ar).

##Game Design
Es una juego SinglePlayer, primera persona en el que debes juntar Chips dispersos por toda una __zona_ de trampas y obstáculos. Cuentas con tres vidas para superar tres niveles de juego.

Existen trampas tales como Minas, que al pasar por encima de ellas explotan en menos de un segundo. Otros Obstáculos como paredes y esferas que se mueven.

Cada en cada nivel aumenta la cantidad de Chips a juntar y la cantidad de Minas, también reduce el tiempo de exploción de las Minas.

Juego SinglePlayer, en primera persona con aspecto de plataformas 3D.

###Plataforma target
El juego fue pensado para plataformas de escritorio desde su inicio.

###Características Especiales
- Detector de minas: __Lerp__ entre rojo y verde de la distancia entre la Mina más cerca al jugador.

**###**

**##Game Development**

**###Características de Motor**
- UI Canvas
- Animaciones de UI
- Partículas: exploción de Minas
- Pooling de Minas y Chips
- No se utilizó Fisicas para movimientos de cualquier objeto

**###Software**
- Patrones: 
  - Singleton: un AudioSource que no se destruye entre escenas. 
  - Estado: Estados de una Mina: Reposo, Contanto, Explición, Detonada.
- Controladores: GameController, CanvasController, PlayerController, SoundController, etc.

##Prueba el juego y envía feedback
El juego está compilado para plataforma **Windows**

Para probarlo, debes descargar el repositorio como Zip, descomprimirlo y ejectuar Build/HardLand.exe

## Espectitivas
EL juego cuenta con poco arte 3D como texturas y objectos. Tamníen los Sprites de UI son báiscos a modo de prototipo.

Debido a esto, se está a disposición de aceptar trabajo artístico de forma libre para mejorar el juego y agregar créditos.

El juego no tiene fines comerciales, sino aplicar prácticas en el mundo de vodeojuegos, compartir conocimientos y experiencias.







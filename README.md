# TrafficDemo

This project consists of a 3D city with dimensions of 30x30 Unity units.

The city is pre-loaded with some assets already in place. The objective of the game is to select objects and place them on the board to build a more habitable city.

Between those objects, exists one, that is a black hole, be carefull with this one... It can cause serious damage to your city!

How to play:

As mentioned before, the game starts with a board representing the playable area, with some vegetation, buildings and roads already in place.
Now, as the player, you have an interface with some of those objects, that by being selected allows you to add them into your personalized city.
To make things more interesting, you can also place a black hole. By being placed, it generates a strong gravitational force that pulls objects toward it and eventually absorbs them.

- Controls:
    - Move the camera forward and backwards: W and S;
    - Move the camera left and right: A and D;
    - Give more altitude to the camera: SPACE BAR;
    - Give less altitude to the camera: LEFT SHIFT;
 
    - To select and place the objects: MOUSE 1 (Left mouse button) on the UI object or wanted cell.
 
Implemented things:
  - A grid system and database to store and map occupied grid cells and the type of data occupying each cell.
  - An Unlit shader graph that toggles the visibility of grid marks (bounds) where objects can be placed.
  - An input system to handle all user inputs, including camera movement, UI interactions, and mouse position.
  - A raycast to determine the impact point, representing the selected cell for object placement.
  - Camera movement;
  - An object placement system that instantiates objects, places them, and saves their data.
  - An Unlit shader graph for a transition effect from the 2D UI to the 3D object, displaying a preview of the object before placement.
  - Implementation of gravitational physics for a black hole and its impact on the city.
  - An Unlit shader graph simulating the black hole’s appearance and the visual distortions it causes on surrounding objects.
  - A UI for selecting objects to be placed in the city.

  Developed work:

  To start this project i started by importing and testing some visual assets and ways to put the initial board and camera angles/prespectives.
  As i dont have VR HeadSets i chose to make a 3d isometric world in order to give, a better sense of overall view of the city, and controls that best fitted that kind of prespective.

  After that i made some sketches as showned one the pictures bellow in order to get a big overview of what the game would need.

  Main Pipeline:
  I started implementing the grid where objects will be placed, and for that, I used Unity's built-in Grid system. The grid is represented by the S_GridData class, which holds the data for each cell's position.
  To manage user inputs, the S_InputManager class is responsible for invoking actions, which, if activated, trigger the corresponding events.
  Two classes are used for placement: S_PlacementManager, which controls the events for placing objects, and S_PlacementPreview, which handles the preview of the object that shows the position where we are trying to place it.
  For camera movement, a MonoBehaviour class is attached to the camera that contains the functions related to camera control. The S_GameManager class manages the camera based on positions provided by the input manager, which checks for any camera movement inputs.
  To control the black hole, there is an S_GravityPoint class that calculates the physics and then applies them to the scene's city objects

  ![Alt Text](./Images/IMG_20250324_233104.png)
  

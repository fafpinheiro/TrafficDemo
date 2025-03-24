# TrafficDemo

This project consistes on a 3d city with dimensions of 30x30 Unity units.

The city is pre-loaded with some assets already in place, and the objective of the game is to select some objects and place them on the board in order to build a more habitable city.

Between those objects exist one that is a black hole, be carefull with this one... It can make real and big damages to your city.

How to play:

As already said, the game starts with a board, whitch represents the playable area, with some vegetation, buildings and roads already in place.
Now as the player you have at your disposal an interface with some of those objects that by being selected can be added to your personalized city.
In addition and to make it more interesting you can also place a black hole, that after being placed it applies a strong gravity force in the objects in the city and enventually absorve them.

- Controls:
    - Move the camera forward and backwards: W and S;
    - Move the camera left and right: A and D;
    - Give more altitude to the camera: SPACE BAR;
    - Give less altitude to the camera: LEFT SHIFT;
 
    - To select and place the objects: MOUSE 1 (Left mouse button) on the UI object or wanted cell.
 
Implemented things:
  - Grid sistem and data base to store and map the occupied grid cells and what type of data is occupying that cell.
  - An Unlit shader graph that shows and hides the grid marks (bounds) where the objects can be placed.
  - Input sistem to handle all the user inputs, like camera moving, UI interaction and mouse position.
  - Raycast to return the inpact point, that representes the cell selected to place the object.
  - Camera movement;
  - Object placemente sistem, that instanciates the object to create and saves its data.
  - 

  Developed work:

  To start this project i started by importing and testing some visual assets and ways to put the initial board and camera angles/prespectives.
  As i dont have VR HeadSets i chose to make a 3d isometric world in order to give, a better sense of overall view of the city, and controls that best fitted that kind of prespective.

  After that i made some sketches as showned one the pictures bellow in order to get a big overview of what the game would need.


So i started implementing the grid where the objects will be placed, for that i used the Grid sistem already built in unity 
  
  

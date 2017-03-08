How to run:
Execute demo.exe.

Controls:
h -> change heuristic number
p -> switch between pov grid and regular grid

NodeBinaryHeap - My implementation uses a custom heap to add and remove nodes. The stack is built in a way that it is always ordered.
When adding a new node, the node is inserted in the stack at the index found to be between nodes with greater and lesser
total distance (based on the heuristic).

Pathfinder - handle the pathfinding logic and select different heuristic based on the player input

Instantiator - Creates the tiles for the file graph

NPC- NPC object that contains logic to make the player move

Brain - Ai part of the player which decides new goals
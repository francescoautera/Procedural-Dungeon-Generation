# Procedural-Dungeon-Generator
Rules of dungeon:

-all rooms have the same size
- each room has a fixed distance from its neighbors
- there is 2 method of creation: variable & fixed
- variable creation, creates room in a range specified in a Scriptable Object Room Rules
- fixed creation, creates rooms until it reaches the number specified in the scriptable  

Presentation of actors:
- Dungeon Manager: create the dungeon
- Neighbor Manager: check neighbors' new room
- Resize Manager: change rooms not completed



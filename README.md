# Procedural-Dungeon-Generator
Rules of dungeon:
- every room has the same size 
- every room has fixed distance from it's neighbors
- there is 2 method of creation: variable & fixed
- variable creation, creates room in a range specified in a Scriptable Object Room Rules
- fixed creation, creates rooms until it reaches the number specified in the scriptable  

Presentation of actors:
- Dungeon Manager: create the dungeon
- Neighbor Manager: check neighbors' new room
- Resize Manager: change rooms not completed



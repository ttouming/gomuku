# Gomuku
## Description

This is a gomuku console app using c#. It provides some basic functions such as CLI interfacem, gomuku game, PvP, PvC, undo, redo, game saving, game loading, and online help link. I also used obejct oriented programming pattern in this project. Firstly, i used Template pattern which is uisg abstract class BoardGame. It is possible to generate other type of boardgame without straing from scratch. Secondly, i used Factory pattern which can generate specific type of player such as human player and computer player in run time.

## User Guide

After opening the application, you can see a main menu.

![image](https://user-images.githubusercontent.com/115144351/203479715-f76fa7ba-62c2-4c57-b23b-466d9d120123.png)

By selecting “a”, you can see sub-menu to choose PvP or PvC.

![image](https://user-images.githubusercontent.com/115144351/203479747-bef17577-f3e8-4103-9eda-2e4a1a5d966d.png)

 By selecting “a”, you can start a PvP game. Game window will look like this.(you can do instructions on the right side by input the corresponding value.)

![image](https://user-images.githubusercontent.com/115144351/203479793-545aef8f-c323-445e-9eb5-8fefcbc4cc3c.png)

 If selecting “b” to load saved game in main menu. You can choose which file to restore.
 
 ![image](https://user-images.githubusercontent.com/115144351/203479830-354cf7bd-bc9b-4faa-81bd-4ccf73694980.png)
 
 The application is using CLI. Sometime, the console window would be dirty. But the function would work well most of the time.

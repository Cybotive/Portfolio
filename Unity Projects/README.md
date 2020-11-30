# Terrain Test:
This project is a proof of concept I made for a general 3D game.
I made this project a bit ago and I used raycasting to know where the player is looking.
My hand-written code is in the directory Assets/Scripts.

## To Run:
Download "Rock Throwing.exe" and the directory "Rock Throwing_Data". Run the exe with the data directory next to it.
If the application requests a firewall rule, you can deny it.

## Controls:
Displayed in the launcher, but basic controls are 'G' to grab a boulder and 'Left Mouse' to throw.
You can run by holding 'Left Shift'.
(the more boulders you are carrying, the slower you get).

# Tower Defense:
This is a more recent project and my main accomplishment was programming the towers to
target the entities walking along the path. I don't have the development build anymore, 
but you can see a lot of what I did by going to Assets/Scripts.

## Targeting Algorithm Explanation:
The targeting algorithm sees if its view is being
obstructed, as well as always choosing the most critical target. I accomplished the algorithm's goal
using waypoints in order to re-use this AI for any map I make.

The majority of the Tower Targeting code is in Assets/Scripts/Tower.cs

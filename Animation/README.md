# Photon-Bolt-Code

This code is meant as an illustration and doesn't work as-is without some changes.

The idea is to have Animator Controller properties/parameters that have no spaces in them, and then define a list of Enums with the exact same names. Then add the enums into IsTrigger for ones that are triggers, so that IsTrigger returns true on those enums.

In my case, I have this code inside the Character class that I use for both my player and mobs.

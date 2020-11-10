# Animation

This code is meant as an illustration and doesn't work as-is without some changes.

The idea is to have Animator Controller properties/parameters that have no spaces in them, and then define a list of Enums with the exact same names. Then add the enums into IsTrigger for ones that are triggers, so that IsTrigger returns true on those enums.

In my case, I have this code inside the Character class that I use for both my player and mobs.

NetworkCallbacks shows the AnimationEventBehavior class and the AnimationEventBehavior.SendEvent() pattern that I use to easily send these events.

Note: You don't want to use Events for something that happens every frame. If you have a float property like a speed or direction that changes often, you want to have those as state properties. You would have those as Animator Properties on your player or character state, and still use this system for other parameters.

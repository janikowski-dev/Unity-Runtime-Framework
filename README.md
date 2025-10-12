# Project Overview

### Quality of Life Tools (Editor)
A set of tools that extend the editor's functionality, making working with Unity easier.

### Dependency Injection System (Runtime/Dependencies)
It’s lightweight and non-invasive, so the project remains functional even after switching architecture. Other systems rely on it for simple and flexible integration.

### Game Loop System (Runtime/Flow)
It uses a simple state machine pattern, making game flow easy to track and modify. Setting it up takes a few minutes at most.

### Custom 2D Collision System (Runtime/Collisions2D)
Unity’s built-in physics can be resource-intensive, so this system is optimized for high-object-count scenarios, such as bullet-hell games. This system replaces the default physics for increased performance.

### Pooling System (Runtime/Pooling)
This system is useful for games with many objects being created and reused.

### Dialogue System (Runtime/Dialogues)
It enables easy conversion of simple `.txt` files into in-game actions, streamlining the creation of dialogue sequences.

### Helper Scripts (Runtime/Utilities)
A collection that includes smaller scripts that are useful across the project but don’t constitute full systems on their own.

### Use Case
You can see most of these tools in action in my project: [Demo](https://github.com/toniezlydeveloper/Demo)

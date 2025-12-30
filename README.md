
## Unity Runtime Framework

A lightweight, modular **runtime framework for Unity** built to speed up development, enforce clean architecture, and scale well in performance-critical projects.

This framework grew out of real production needs — reusable systems I used across multiple projects to avoid rewriting core logic and to keep projects flexible as they evolved.

## Installation

This package is distributed as a **Unity Package (UPM)** and can be installed directly from Git.

### 1. Install dependency (LitMotion)

This framework depends on **LitMotion** for tweening.

1.  Open **Unity**
2.  Go to **Window → Package Manager**
3.  Click **➕**
4.  Select **Add package from git URL…**
5.  Paste:
    `https://github.com/annulusgames/LitMotion.git?path=src/LitMotion/Assets/LitMotion` 
6.  Click **Install**

### 2. Install the framework

1.  Click **➕** in the Package Manager
2.  Select **Add package from git URL…**
3.  Paste:
    `https://github.com/janikowski-dev/Unity-Runtime-Framework.git` 
4.  Click **Install**

## Key Design Goals

-   **Runtime-first** – focused on game code, not editor gimmicks
-   **Low coupling** – systems are independent and easy to replace
-   **Minimal overhead** – avoids unnecessary Unity abstractions
-   **Drop-in friendly** – adopt individual systems without committing to everything
    
## Included Systems

### Quality of Life Tools (Editor)

A small set of editor extensions that streamline common workflows and reduce friction during development.

### Dependency Injection System (Runtime / Dependencies)

A lightweight, non-invasive DI system used to wire runtime systems together.

-   No heavy containers
-   Minimal boilerplate
-   Safe to remove or replace if architecture changes
-   Used internally by other systems, but not required

### Game Loop System (Runtime / Flow)

A simple, explicit **state-machine-based game loop**.

-   Easy to reason about and debug
-   Clear separation of game states
-   Can be set up in minutes

### Custom 2D Collision System (Runtime / Collisions2D)

A performance-oriented alternative to Unity’s built-in 2D physics.

-   Designed for **high object counts** (e.g. bullet-hell, arcade games)
-   Deterministic and lightweight
-   Replaces default physics where performance matters most

### Pooling System (Runtime / Pooling)

A generic object pooling solution for managing frequently spawned objects.

-   Reduces GC pressure
-   Simple, allocation-conscious API

### Dialogue System (Runtime / Dialogues)

A data-driven dialogue system based on plain text files.

-   Converts `.txt` files into in-game actions
-   Fast iteration for narrative content
-   Keeps dialogue authoring simple and engine-agnostic

### Helper Scripts (Runtime / Utilities)

A collection of small, reusable utility scripts that support the framework but don’t form standalone systems.

## Intended Audience

This framework is aimed at:

- Indie and solo developers
- Gameplay programmers
- Technical programmers working close to runtime systems
    
It’s not meant to replace Unity packages — it’s a **foundation you build on**.

# Unity Runtime Framework

A lightweight, modular **runtime framework for Unity**, built around reusable runtime systems used in real projects.

It focuses on reducing repeated boilerplate, keeping systems loosely coupled, and maintaining performance as projects grow — without trying to be a “do-everything” framework.

---

## Why This Exists

After working on multiple Unity projects, the same problems kept coming up:

- rewriting basic runtime systems from scratch  
- tightly coupled logic that became hard to refactor  
- Unity abstractions that added overhead without much benefit  
- systems that worked early, but didn’t scale well  

This framework grew out of solving those problems in a practical way — keeping things **explicit**, **modular**, and **easy to remove or replace** when requirements change.

---

## Installation

The framework is distributed as a **Unity Package (UPM)** and can be installed directly from Git.

### 1. Install dependency (LitMotion)

The framework uses **LitMotion** for tweening.

1. Open **Unity**
2. Go to **Window → Package Manager**
3. Click **➕**
4. Select **Add package from git URL…**
5. Paste:
   ```
   https://github.com/annulusgames/LitMotion.git?path=src/LitMotion/Assets/LitMotion
   ```
6. Click **Install**

### 2. Install the framework

1. Click **➕** in the Package Manager
2. Select **Add package from git URL…**
3. Paste:
   ```
   https://github.com/janikowski-dev/Unity-Runtime-Framework.git
   ```
4. Click **Install**

---

## Design Principles

- **Runtime-first**  
  Focused on gameplay and runtime systems, not editor tooling.

- **Low coupling**  
  Systems are independent and communicate through clear boundaries.

- **Minimal overhead**  
  Avoids heavy abstractions and unnecessary Unity wrappers.

- **Pick what you need**  
  Individual systems can be used without adopting the whole framework.

---

## Included Systems

### Quality of Life Tools (Editor)

A small set of editor helpers that reduce friction during development.

They are intentionally minimal and not required by the runtime systems.

---

### Dependency Injection (Runtime)

A lightweight, non-invasive DI approach used to wire runtime systems together.

- No heavy containers  
- Easy to remove or replace if architecture changes  
- Used internally, but never required  

---

### Game Loop (Runtime)

A simple, explicit **state-machine-based game loop**.

- Easy to reason about  
- Clear separation of game states  
- Straightforward debugging  
- Designed to be set up quickly and extended as needed  

---

### Custom 2D Collision System (Runtime)

A performance-oriented alternative to Unity’s built-in 2D physics.

- Designed for **high object counts** (e.g. bullet-hell, arcade-style games)  
- Deterministic and lightweight  
- Intended for cases where default physics becomes a bottleneck  

---

### Object Pooling (Runtime)

A generic pooling system for frequently spawned objects.

- Reduces Instantiate pressure  
- Simple, allocation-conscious API  
- Can be used independently of other systems  

---

### Dialogue System (Runtime)

A data-driven dialogue system based on plain text files.

- Converts `.txt` files into in-game actions  
- Fast iteration for narrative content  
- Keeps dialogue authoring simple and engine-agnostic  

---

### Utilities (Runtime)

A collection of small helper scripts that support the framework but don’t form standalone systems.

These exist to reduce repeated boilerplate rather than introduce new abstractions.

---

## Intended Audience

This framework is meant for:

- Indie and solo developers  
- Gameplay programmers  
- Technical programmers working close to runtime systems  

It’s **not** meant to replace existing Unity packages or impose a fixed architecture.

Think of it as a **runtime toolbox** — something you build on, adapt, or partially adopt depending on the project.

# 🌊 Umi No Sakuragi

![Unity](https://img.shields.io/badge/Engine-Unity-ff9c00?logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/Language-C%23-239120?logo=csharp&logoColor=white)
![Status](https://img.shields.io/badge/Stage-Prototype-blueviolet)
![License](https://img.shields.io/badge/License-MIT-green)

> A historical simulation prototype built with Unity3D and C#

---

## 📚 Table of Contents

- [🎮 Overview](#-overview)
- [🎨 Art & Asset Creation](#-art)
- [🕹️ Features](#-features)
- [📸 Media](#-media)
- [🧩 Technical](#-technical)
- [📐 Class & Flow Diagrams](#-class--flow-diagrams)
- [🚀 Getting Started](#-getting-started)
- [🔗 References](#-references)

---

## 🎮 Overview

**Umi No Sakuragi** (海の桜木) is a **first-person simulation** set during WWII on the legendary Japanese battleship **Yamato**.  
You explore as a shipboard tailor collecting memories through objects scattered across the environment. The project is a **prototype focused on movement, interaction, and atmosphere.**

---

## 🎨 Art & Asset Creation

### Character 
What I do ?
- Riging texturing and animation

<img width="2980" height="1098" alt="image" src="https://github.com/user-attachments/assets/4dc3b4f1-09c3-4a8b-8b63-36ca0a5c6182" />

- Rig

<img width="535" height="681" alt="image" src="https://github.com/user-attachments/assets/cac0d88b-c13c-43e4-9168-4b8f8097602b" />


---

## 🕹️ Features

- 🧍‍♂️ First-Person Controller (WASD + mouse)
- 🚶 Idle, Walk, Run (Shift) animations via Blender
- 🪙 Interact with objects:  
  - `P` → Pick  
  - `D` → Drop  
  - `T` → Throw  
  - `R` → Rotate  
- 💡 Physical flashlight
- 🎥 Full 3D camera control
- 🧠 Expandable interaction logic for memory collection

---

## 📸 Media

> Demo video and screenshots

<img width="1371" height="722" alt="image" src="https://github.com/user-attachments/assets/b16f6cc5-0601-4788-b976-a0c096c5207d" />

### 🎬 Video Preview  
[![Watch Demo](https://img.youtube.com/vi/YOUTUBE_ID/0.jpg)](https://www.youtube.com/watch?v=YOUTUBE_ID)

### 🖼️ Screenshots

| Character View | Ship Interior | Object Hold |
|----------------|---------------|--------------|
| ![](assets/images/char_front.jpg) | ![](assets/images/ship_interior.jpg) | ![](assets/images/holding_obj.jpg) |

---

## 🧩 Technical

| Component        | Description                                |
|------------------|--------------------------------------------|
| `PlayerMotor`    | Handles movement, gravity, jump, speed     |
| `PlayerLook`     | Mouse-controlled camera                    |
| `InputManager`   | Unity Input System connector               |
| `PlayerInteraction` | Object pickup/throw/drop/rotate          |
| Animations       | Blender-imported: idle, walk, run          |

---

## 📐 Class & Flow Diagrams

### 🧭 UML

<img width="1228" height="864" alt="image" src="https://github.com/user-attachments/assets/74d52069-5902-4058-a158-78f23f6d9faa" />



![Gameplay](https://youtu.be/meX_4ot7IJo?si=eUbN62Z8g1xqDfff)

![Trailer Game](https://youtu.be/UmY9JF-NDzo?si=ZdY0qVYsRo_1NNny)



# Adaptive Bubble Selection for Virtual Reality

This repository contains the core C# implementation of an adaptive, bubble-based object selection technique developed for a Human–Computer Interaction (HCI) class project.

The code supports the exploration and comparison of multiple VR selection strategies, including raycasting, fixed-radius bubble selection, and an adaptive bubble-based approach that responds to user motion and local spatial density.

---

## Project Overview

Object selection in virtual reality (VR) becomes increasingly challenging in dense or cluttered environments due to depth ambiguity, occlusion, and involuntary hand motion.  
This project investigates how adaptive bubble-based selection strategies can improve robustness and stability compared to conventional techniques.

The implementation focuses on selection logic, intent scoring, and interaction behavior, rather than on visual design or application-specific content.

---

## Scope of This Repository

This repository contains **code only**.

Specifically, it includes:
- C# scripts implementing VR object selection logic
- Core algorithms for proximity-based and adaptive bubble selection
- Supporting logic for switching, scoring, and interaction state handling

Scene files, assets, experimental data, and survey materials used during the study are not included in this repository.
---

## Requirements

- Unity 2021 or later (tested with Unity 2021 LTS)
- A VR-capable setup (e.g., Meta Quest 2 or compatible OpenXR device)

---

## Notes

This repository is intended for educational and research purposes.  
The code can be integrated into a Unity VR project to reproduce or extend adaptive bubble-based selection behaviors.
Unity `.meta` files are included where applicable, as they are required for correct asset identification and reference consistency within Unity-based projects.

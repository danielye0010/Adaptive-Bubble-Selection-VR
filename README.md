# Adaptive Bubble Selection for Virtual Reality

This repository contains the core C# interaction logic for an adaptive bubble-based VR object-selection technique developed as a Human-Computer Interaction (HCI) class project.

The project compares conventional selection behaviors such as raycasting and fixed-radius bubbles with an adaptive approach that changes selection behavior using **hand motion, target proximity, and local target density**.

## Core idea

Dense VR scenes can make object selection unstable because nearby targets compete for attention and small hand motions can change the apparent selection intent. The adaptive selector scores candidate targets using a combination of:

- distance from the interaction point,
- direction of recent hand/controller movement,
- local target density,
- dynamically adjusted distance/direction weights.

`IntentSelector.cs` contains the main intent-scoring logic. In denser regions, the weighting shifts toward proximity; in sparser regions, movement direction contributes more strongly to the selection score.

## Repository scope

This is a **code-only research prototype**, not a complete Unity project. It contains the C# scripts and their Unity `.meta` files, but intentionally does not include:

- Unity scenes or prefabs,
- 3D assets,
- experimental participant data,
- survey materials,
- study-specific build files.

The scripts are intended to be integrated into an existing Unity/OpenXR project.

## Main scripts

| Script | Role |
|---|---|
| `IntentSelector.cs` | Scores all `BubbleTarget` objects using distance, movement direction, and local density; identifies and highlights the current best target. |
| `BubbleTarget.cs` | Marks selectable bubble targets and maintains target-specific runtime material state. |
| `BubbleScaler.cs` | Smoothly changes bubble scale as a function of hand-to-target distance and highlight state. |
| `BubbleReveal.cs` | Supporting reveal/visibility behavior for bubble interactions. |
| `Collectible.cs` | Interaction behavior for collectible/selectable objects. |
| `AdaptiveCollectible.cs` | Adaptive variant of collectible interaction behavior. |
| `HandSelectionTimeUI.cs` | Displays interaction/selection timing information in the VR UI. |

Additional scripts in the repository support the alternative selection conditions and interaction-state logic used by the prototype.

## Requirements

- Unity 2021 LTS or later
- TextMeshPro
- A VR/OpenXR-capable Unity setup
- A compatible headset/controller setup such as Meta Quest 2

The original prototype was developed around Unity 2021-era tooling; package/API adjustments may be needed in newer Unity releases.

## Integration outline

1. Copy the scripts and their `.meta` files into the `Assets/` tree of a Unity project.
2. Add `BubbleTarget` to objects that should participate in bubble selection.
3. Attach `IntentSelector` to the hand/controller transform used as the interaction point.
4. Connect the optional TextMeshPro score UI in the Inspector.
5. Configure the range/weight parameters for the desired interaction condition.
6. Attach supporting collectible/scaling scripts to the relevant target prefabs.

### Important Inspector setting

`IntentSelector.detectRange` and `BubbleScaler.detectRange` represent the selection/scaling interaction range. Their defaults in the original scripts are different (`2.0` and `0.6` respectively), even though the scaler was written with the expectation that these values are configured consistently for the intended experiment. **Set them deliberately in the Unity Inspector rather than relying on both defaults.**

The repository leaves the original numeric defaults unchanged so that documenting the project does not silently alter the experimental behavior.

## Research use

The code is useful for exploring adaptive target-selection behavior, interaction stability in cluttered 3D scenes, and intent scoring based on motion and spatial context. It can also serve as a starting point for controlled comparisons with raycasting or fixed-radius bubble techniques.

## Notes on `.meta` files

Unity `.meta` files are intentionally tracked because Unity uses their GUIDs to maintain asset/script references. The repository-level `.gitignore` therefore ignores generated Unity build/cache directories while keeping `.meta` files version-controlled.

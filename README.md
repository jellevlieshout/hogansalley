# Tomatina Alley
**Game Documentation**  
*By Jelle Van Lieshout & Rashida Dudhiawala*

**Public Repository Link:** https://github.com/jellevlieshout/hogansalley
---

## Game Overview
**Tomatina Alley** is a short VR throwing game inspired by Spain’s La Tomatina festival.  
The player stands in a small alley and throws tomatoes from their hands at stationary targets to eliminate them and clear the scene.  
The game is about scoring as many points as possible until the countdown ends.

---

## 1. Scenes, Assets, GameObjects and Components

### 1.1 Main Scene
- **Scene name:** `TomatinaAlley` (formerly based on Hogan’s Alley main scene)  
- **Main top-level objects:**

#### XR Origin (Rig)
- **Components:**
  - `XROrigin` (or XR Rig)  
  - `CharacterController` (for collision)  
  - `Input Action Manager` (references XRI Default Input Actions)  
- **Children:**
  - **Main Camera**
    - Components: `Camera`, `AudioListener`, `TrackedPoseDriver`  
  - **LeftHand Controller**
    - Components: `XR Controller` (Action based), optional model/hand mesh  
  - **RightHand Controller**
    - Components: `XR Controller` (Action based), `HandTomatoShooter` script  
    - **Child:** `TomatoSpawn` (empty Transform at palm/fingertips)  

#### Environment
- **Ground** (Plane or Mesh)
  - Components: `MeshRenderer`, `MeshCollider`  
- **Walls/Props**
  - Static meshes with `MeshRenderer`, `BoxCollider` or `MeshCollider`  
- **Background/Skybox:** handled via Lighting Settings or skybox material  

#### Targets (Stationary or Moving Objects)
- Examples: `TomatinaTargetCube`, …  
- **Components:**
  - `MeshRenderer` + `Collider`  
  - `Rigidbody` (optional for knockback)  
  - `RandomTimedTarget` (custom script to track hits and elimination)  
  - Optionally: `MovingTarget`
    
#### Game Manager
- **GameManager GameObject** with `GameManager` script:
  - Tracks total score  
  - Listens for target elimination events  
  - Ends the scoring when timer is over  
  - Manages game state (`Ready → Playing → Time is up`)  

#### UI/HUD (optional)
- **World-space Canvas:**
  - `TextMeshProUGUI` for score, remaining targets, or instructions  
- **Components:**
  - `Canvas` (World Space), `CanvasScaler`  
  - `XR UI Input Module` if interactive UI is used  

---

## 2. Interaction, Mechanics and Scripts

### 2.1 Core Mechanics
- **Tomato throwing from hands:**
  - Right controller functions as a hand that spawns tomato projectiles.  
  - Pressing the mapped Shoot action instantiates a tomato prefab at the `TomatoSpawn` point and launches it forward.  
- **Hitting and eliminating targets:**
  - Tomatoes use physics and collide with targets.  
  - On collision, targets register a hit and are considered eliminated.  
  - The Game Manager updates score.  

### 2.2 Main Scripts

#### 2.2.1 HandTomatoShooter
- **Attached to:** RightHand Controller  
- **Key fields:**
  - `InputActionProperty shootAction` – bound to XRI RightHand Interaction/Shoot  
  - `Transform spawnPoint` – reference to `TomatoSpawn` child transform  
  - `GameObject tomatoPrefab` – prefab of the tomato projectile  
  - `float shootForce` – strength of the initial tomato impulse  
- **Responsibility:**
  - Subscribes to the shoot input action  
  - On input performed:
    - Instantiates `tomatoPrefab` at `spawnPoint.position`  
    - Applies force or velocity to launch the tomato forward  

#### 2.2.2 TomatoProjectile
- **Attached to:** Tomato prefab  
- **Responsibility:**
  - On `OnCollisionEnter`, notifies hit target (via interface or direct component lookup)  
  - Destroys itself after collision  

#### 2.2.3 GameManager
- **Attached to:** GameManager object  
- **Fields:**
  - Score counter  
  - Timer  
  - Reference to UI elements for status display  
- **Responsibility:**
  - Counts targets and initializes game state on start  
  - Receives callbacks when a target is eliminated  
  - Updates score  
  - Ends scoring when the timer is over  

---

## 3. Input and Interaction Setup
- **Input System:** Unity’s New Input System  
  - XRI Default Input Actions asset  
  - Input Action Manager referencing that asset  
- **XR Controllers (Action-based):**
  - **Right hand:**
    - Select/Activate mapped to trigger  
    - Custom Shoot action mapped to right trigger  
    - `HandTomatoShooter.shootAction` bound to Shoot action to call `ShootTomato()`  

---

## 4. How to Play

### 4.1 Game Elements
1. **Player:** VR user standing in an alley during a La Tomatina inspired festival  
2. **Tomatoes:** Physics-based projectiles spawned from the player’s hand  
3. **Targets:** Stationary characters or props; eliminated by hitting with a tomato  
4. **Environment:** Stylized alley with buildings and walls  

### 4.2 Controls
- **Look around:** Move your headset  
- **Aim:** Rotate/move controller hand pointing at a target  
- **Shoot tomato:** Press the configured Shoot input (usually right trigger)  

### 4.3 Objective and Flow
1. Enter the scene and observe the alley and stationary or moving targets  
2. Aim your hand at a target  
3. Press the shoot button to launch a tomato  
4. Hit each target to eliminate it  
5. Continue until the timer ends

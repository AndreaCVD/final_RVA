# 🍕 ParaParlaPizza

**A VR cooking chaos simulator where language is the secret ingredient.**

---

## 🎮 About the Game

**ParaParlaPizza** is a first-person VR simulation and management game that combines fast-paced cooking mechanics with language learning. You work in a food truck, taking orders from customers who speak different languages. Using a visual dictionary that show you foreign words, you must prepare pizzas correctly while managing kitchen chaos.

The game is designed for players who want to learn basic vocabulary in a fun, immersive way without needing prior language knowledge. It blends elements from *Overcooked* (kitchen chaos), *Cook, Serve, Delicious!* (order management), and *Five Nights at Freddy's* (tutorial style).

**Target Audience:** Young adults and adults interested in VR experiences, casual gaming, and learning languages in an entertaining context.

**Sector:** Educational entertainment (edutainment) with casual simulation mechanics.

---

## 📖 Story

You've just been hired at a food truck. Your boss, calls you on the first day:

> *"Bon dia! I'm so happy I found you. Not many people speak so many languages! The previous employee was a disaster... he left the oven broken and burned everything. If that happens to you, consider yourself dead! The plug is on a table somewhere. Find it and start the day. And remember: close on time, or else..."*

You lied on your resume — you don't actually speak multiple languages. Now you have to survive using a visual dictionary and quick thinking. Each day, customers arrive with orders in different languages. You must understand what they want, prepare the pizza, handle kitchen disasters (rats, broken oven), and deliver on time.

---

## 🎯 Gameplay

**Core Loop:**

1. **Customer arrives** – They place an order in a foreign language (e.g., Italian).
3. **Prepare the pizza** – Grab the dough, add sauce, and place the correct ingredients.
4. **Cook the pizza** – Put it in the oven and wait for it to bake.
5. **Deliver** – Place the pizza in the delivery box to serve the customer.
6. **Score** – Earn points based on accuracy and speed. Unresolved handicaps deduct points.

**Key Features:**

- 🗣️ **Language Learning** – Learn basic food vocabulary in Italian (future levels: German, French, English, Catalan, etc.).
- 🎨 **Visual Dictionary** – Ingredient names appear in the level lenguage.
- 🪳 **Dynamic Handicaps** – Random events during orders (rats) that you must resolve by hitting them.
- ⏱️ **Time Pressure** – Each order has a timer. Deliver before it runs out!
- 🍳 **Realistic VR Cooking** – Knead dough, spread sauce, grate cheese, slice ingredients, and operate the oven.

---

## 🎮 Controls (Meta Quest 3)

| Action | Controller | Button |
|--------|-----------|--------|
| Move | Left Stick | Analog Stick |
| Turn | Right Stick | Analog Stick |
| Grab object | Either hand | Grip (side button) |
| Interact / Select | Either hand | Trigger |
| Throw / Release | Either hand | Release Grip |
| Cancel / Back | Either hand | B / Y |
| Accept / Answer phone | Either hand | A / X |

**Advanced Interactions:**

| Action | How to perform |
|--------|----------------|
| **Kill rats** | Grab spatula and hit the rats |

## 📊 Scoring System

Points are deducted for:

- Wrong or missing ingredient
- Unresolved handicap (rat, broken oven) 
- Late delivery (after timer expires)
- Customer timeout (no delivery) 

**Example Score Calculation:**

- Order: 4 ingredients (tomato, cheese, basil, olive oil) = 100 base points
- Player puts: tomato, cheese, pepperoni (wrong), misses basil → 2 errors = -50 points
- Unresolved rat handicap = -20 points
- **Total = 30 points**

---

## 🪳 Handicaps (Kitchen Disasters)

During an order, **up to 2 handicaps** can appear randomly. You must resolve them **before delivering the pizza**, or they will deduct points. Handicaps persist across orders if not resolved.

**Current Handicaps:**

| Handicap | Behavior | Resolution | Hits Required | Penalty |
|----------|----------|------------|---------------|---------|
| 🐀 **Rat** | Runs around the food truck floor randomly, changes direction frequently | Hit it with a spatula (or any object with tag `Destroyer`) | 3–8 hits (random) | -20 points |

**Handicap Resolution System:**

- Each handicap requires a random number of hits (within a defined range).
- Once resolved, the handicap disappears or deactivates.
- Unresolved handicaps persist to the next order and continue to penalize.

## 🚀 Future Scalability

**Language Levels (Planned):**

The game is built to support multiple language levels. Each level would change customer dialogue to a new language, update the visual dictionary with new vocabulary, introduce culture-specific pizzas, and keep the same core mechanics.

- **Currently implemented:** Italian 
- **Planned:** German, French

**Handicap System (Easily Expandable):**

The handicap system uses the `IHandicap` interface, allowing new handicaps to be added by creating a new script that implements `IHandicap`, adding the prefab to `HandicapManager.handicap_prefabs` (for spawnable handicaps), or adding a reference in `HandicapOrderManager` (for fixed handicaps). No core code changes required.

**Potential Future Handicaps:**

| Handicap | Behavior | Resolution | Hits Required |
|----------|----------|------------|---------------|
| 🪳 **Cockroach** | Very fast, small hitbox, changes direction rapidly | Hit with spatula | 2-5 hits |
| 🐜 **Ants** | Slow but multiply if not killed quickly | Hit individual ants | Variable |
| 🐌 **Slug** | Leaves a slippery trail that affects player movement | Hit the slug | 5-10 hits |
| 🪰 **Fly** | Flies in the air, requires vertical aiming | Hit in mid-air | 1-3 hits |
| ❄️ **Frozen ingredients** | Ingredients frozen solid, cannot be used until thawed | Use blowtorch or hit to break | 3-5 hits |
| 🧂 **Salt/Sugar confusion** | Visually identical containers, player must remember positions | Identify correctly | N/A |
| 🍄 **Moldy cheese** | Cheese has mold that must be cut off | Cut mold with knife | N/A (precision-based) |

**Difficulty Scaling (Planned):** More customers per day (10 → 15 → 20), shorter timers (30s → 25s → 20s), more complex recipes (3 → 4 → 5 ingredients), multiple handicaps simultaneously (2 → 3 → 4), new ingredient types unlocking each day.

---

## 👥 Development Team

| Role | Members | Responsibilities |
|------|---------|------------------|
| **Programmers** | Aina, Andrea, Laia, Paula | NPC dialog system, order timer, pizza mechanics, oven functionality, delivery system |
| **3D Artists / Modelers** | Arnau, Júlia | Art direction, asset integration, scene building |
| **UI Design** | Riu, Martina | Scene creation, in-game interface, menu systems |
| **Sound Design** | Arnau, Júlia | Music, VFX sounds, NPC voice, SFX |
| **Documentation** | Riu | Documentation, team coordination, communication |

**Development Tools:** Git & GitHub (version control with branch-based workflow), Google Documents (task management with deadlines and responsibilities and documentation and file sharing), Notion (sprint planning and milestone tracking).

---

## 🛠️ Technologies Used

- **Unity 2022.3+** with **XR Interaction Toolkit**
- **Meta Quest 3** (also works with XR Device Simulator for PC testing)
- **Universal Render Pipeline (URP)**
- **OpenXR** – Cross-platform VR standard
- **Git & GitHub** – Version control
- **C#** – Scripting language

---

## 💻 Download & Installation

### Download the Game

Go to the **Releases** section of our GitHub repository and download the latest build.

### Installation Instructions

**For Meta Quest (Standalone):**

1. Download the `.apk` file.
2. Install **SideQuest** or **Meta Quest Developer Hub** on your PC.
3. Connect your Quest to your PC via USB.
4. Sideload the `.apk` using SideQuest (drag and drop).
5. On your Quest, go to **Apps → Unknown Sources** and launch **ParaParlaPizza**.
---

## 🎮 How to Play (Step by Step)

**1. Start the Game:** Launch the game from the executable or APK. Hit play. You will appear inside the food truck with the blinds down. The phone will ring. **Grab the phone** (use Grip button) to answer.

**2. Tutorial (First Day Only):** Pick up the phone. Listen to your boss and press the button so the blinds go up. The day begins!

**3. Take an Order:** A customer appears at the window. Pressing Y you can view the ticket. The ticket shows ingredient names in **Italian** (e.g., `pomodoro` with a tomato silhouette). A **30-second timer** starts. You have limited time to prepare and deliver.

**4. Prepare the Pizza:** Grab the dough from the counter. Add sauce (use the sauce bottle or tilt the tomato sauce container). Add ingredients based on the order: grab ingredients from the refrigerator or counter, cut, tear, or sprinkle them as needed, and place them on the pizza (they will snap into place automatically). The order ticket updates as you add ingredients (items get crossed out).

**5. Handle Handicaps (if they appear):** 
- **Rat:** It will run around the floor randomly. Hit it with the spatula (or any object with tag `Destroyer`). After 3-8 hits (random), it disappears.
- If you don't resolve handicaps before delivering, you lose **20 points** per handicap.

**6. Cook the Pizza:** Open the oven door. Place the pizza inside. Wait for the cooking time. Remove the pizza.

**7. Deliver the Pizza:** Place the pizza in the **Pizza Box**. The order is automatically validated. Correct ingredients give full points. Wrong or missing ingredients deduct points per error. Unresolved handicaps add extra deduction.

**8. End of Day:** After 10 customers, the red button on the table glows again. Press it to finish the day. View your **score summary** (total points earned, stars per language). Press the button again to start the next day.

---

## 🧪 Testing Without VR Headset

If you don't have a VR headset, you can test the game using the **XR Device Simulator** (included with XR Interaction Toolkit). This is useful for debugging and development.

**Controls:**

| Action | Key / Mouse |
|--------|-------------|
| Move forward/back/left/right | WASD |
| Turn left/right | Q / E |
| Move up/down (vertical) | Space / C |
| Switch active hand | Tab |
| Grab object (when hand selected) | G (hold) |
| Release object | Release G |
| Click / Interact (trigger) | Left mouse click |
| Aim (ray cursor) | Mouse movement |

**Note:** The simulator only works when running the game from the Unity Editor, not from a built executable. If you downloaded the built version, you need a VR headset to play.

---

## ⚠️ Known Limitations (Prototype v1.0)

| Limitation | Description | Future Plan |
|------------|-------------|--------------|
| **Only Italian level** | Only one language is currently implemented | Add German, French, English, Catalan via ScriptableObjects |
| **1 handicap active** | Only rat is fully implemented | Add cockroach, fly, ants, slug |
| **No persistent save system** | Progress resets when game closes | Add save/load system |
| **Limited customer number** | Incrementnumber of customers | Expand with full dialogue system |

---
## 📄 License

This project was developed for educational purposes as part of the **Virtual Reality and Augmented Reality** course at **CITM - UPC (Center for Image and Multimedia Technology)**.

**Course:** Realitat Virtual i Augmentada  
**Academic Year:** 2025-2026  
**Group:** 4

---

## 🍕 Enjoy the game! **Buon appetito! 🇮🇹**

---

### 📎 Links

- **GitHub Repository:** [https://github.com/AndreaCVD/final_RVA ](https://github.com/AndreaCVD/final_RVA )
- **Pinterest (Moodboard):** [https://pin.it/WGDXLXIdS ](https://pin.it/WGDXLXIdS )

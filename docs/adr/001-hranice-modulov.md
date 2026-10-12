001

**Context**
  Creation of a new project.

**Decision**
  Project will be split into 6 parts.
    1. Data - Every data about the game, enemies, waves, weapons. Does not know about any other part.
    2. Gameplay - Controller for player and enemies. Does not know about presentation and Editor. Knows about Data.
    3. Presentation - Controlls UI of the game. Knows about Data and Gameplay.
    4. Editor - Editor tool that make changing/adding things easier.
    5. Test - Tests for testing parts of the game. Not compiled into the final build.

**Consequences** 
  Can edit data of anything without touching any code.

  **Status**
    Awaiting approval

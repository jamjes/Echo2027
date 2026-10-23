# ECHO — 2D Time-Manipulation Platformer Framework
> ECHO is a high-performance 2D platformer framework built in Unity 6 (C#), designed to showcase deterministic frame-accurate time manipulation and tight player movement controls.

## 🛠️ Key Technical Features
### 1. Custom Circular Buffer Time-Rewind System
- Designed a zero-allocation, fixed-capacity circular buffer in C# to record player transform states, velocity, sprite states, and input flags per frame.
- Bypassed `List<T>` overhead and garbage collection spikes during rewind execution by reusing pre-allocated array memory blocks.
- Built state interpolation logic between recorded frames to maintain smooth visuals regardless of framerate variance.

### 2. Tight Game Feel & Rigidbody2D Mechanics
- Custom physics wrapper on `Rigidbody2D` implementing variable jump height, apex gravity modifiers, corner-correcting raycasts, and dynamic coyote time.
- Decoupled player input handling using C# events to ensure clean separation between input reading (`Update`) and physics resolution (`FixedUpdate`).

### 3. Modular Architecture
- Event-driven architecture eliminating tight coupling between core gameplay loops, UI events, and audio triggers.

## ⚡ Code Snippet: Circular Buffer State Recording
``` c#

public class TimeBuffer<T>
{
    private readonly T[] _buffer;
    private int _head;
    private int _tail;

    public int Capacity { get; }
    public int Count { get; private set; }

    public TimeBuffer(int capacity)
    {
        Capacity = capacity;
        _buffer = new T[capacity];
    }

    public void Push(T item)
    {
        _buffer[_head] = item;
        _head = (_head + 1) % Capacity;

        if (Count < Capacity)
            Count++;
        else
            _tail = (_tail + 1) % Capacity;
    }

    public T Pop()
    {
        if (Count == 0) throw new InvalidOperationException("Buffer is empty.");

        _head = (_head - 1 + Capacity) % Capacity;
        Count--;
        return _buffer[_head];
    }
}

```

## 🏆 Awards & Recognition
- **1st Place** for **Technical Design (GradEx)** — Awarded for software architecture, clean C# design patterns, and framework responsiveness.

## 🚀 How to Run locally
1. Clone the repository:

``` bash

git clone https://github.com/jamjes/ECHO.git

```

2. Open the project in **Unity 6 (6000.0.x)** or later.
3. Open `Assets/Scenes/Main.unity` and press Play.

## 👤 Contact
- **Developer**: Jam Jesurasa
- **Portfolio**: [jamjes.github.io/portfolio](https://jamjes.github.io/portfolio/)
- **LinkedIn**: [linkedin.com/in/jamjesurasa](https://linkedin.com/in/jamjesurasa)
- **Location**: West Midlands, UK

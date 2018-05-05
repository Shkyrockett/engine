# Architecture Thoughts

Some architectural trees for working out how things should work.

## Game Tree

- Game
  - Resources (Dictionary)
    - Vector Images
    - Vector Masks
    - Animation Keys
    - Midi Music
    - Audio Recordings
    - Dialog Text
    - Scripts
  - Objects (Dictionary)
    - Vector Images
    - Collision Shapes (Vector Masks)
    - Animation Keys
    - Key Value Tags
  - Characters (Dictionary)
    - Object
    - Verbs
  - Rooms (Dictionary)
    - Objects (QuadTree)
    - Characters (QuadTree)

## Vector Image Tree

- Container (Extended Group)
  - File Name
  - Name
  - Meta-data
  - Styles (Dictionary)
    - Style (List)
      - Fill (Solid/Gradient)
      - Stroke
        - Fill (Solid/Gradient)
        - Thickness
        - Alignment
        - Start Cap (Shape)
        - End Cap (Shape)
        - Dash pattern
        - Dash Cap (Shape)
        - Join Cap (Miter/Shape)
        - Miter Limit
  - Items (QuadTree)
    - Shape (Line/Ray/Rectangle/Polygon/Polycurve)
      - Name
      - Style
      - Meta-data
      - Geometry (List)
    - Writing
      - Text
      - Font
      - Positioning
    - Group
      - Name
      - Meta-data
      - Items (QuadTree)
        - Shape
        - Writing
        - Group
  - Render
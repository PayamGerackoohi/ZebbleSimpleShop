# Version 1.0.0
## Favorites List
- On Nav back on the detailed product page, when it's removed from the favorites list, the removed view would stay in the list, although ListView claims that both the DataSource and children count are decreased, but it renders out of the canvas and ClipChildren = true even would be reset to false and no idea why does not work.

## Scroll Issue
- Mouse or drag by pointer sometimes does not work, specially when scrolling over an inner row. It's interesting that both trackpad and mouse wheel could easily scroll everywhere, even in the before mentioned areas.

## UI Margins
- UI margin is sometimes ignored in rendering. AwaitNative addition is tried, but seems not relevant.

## No Symbols Loaded
- Seldom it happens that app crashed meanwhile navigating. Visual Studio claims: "System.NullReferenceException: 'Object reference not set to an instance of an object.'". Unfortunately there is no clear error log, which could describe where it happened. By the way it's tried to fix any cavity which could make this null reference blindly in the whole app, but still it crashes.
- "Zebble.BoxShadow.pdb not loaded". No idea what it means.

## ItemPicker UI
- ItemPicker dialog sometimes does not wrap content as it supposed to be.

## App Halt
- It seems strolling in app would fill the memory up and halt and exit the app. No idea why it happens and how to solve it.



# Version 1.0.0
## Favorites List
- On Nav back on the detailed product page, when it's removed from the favorites list, the removed view would stay in the list, although ListView claims that both the DataSource and children count are decreased, but it renders out of the canvas and ClipChildren = true even would be reset to false and no idea why does not work.

## Scroll Issue
- Mouse or drag by pointer sometimes does not work, specially when scrolling over an inner row. It's interesting that both trackpad and mouse wheel could easily scroll everywhere, even in the before mentioned areas.

## UI Margins
- UI margin is sometimes ignored in rendering. native addition is tried, seems not relevant.

# HidePart

A lightweight Kerbal Space Program (KSP) mod that streamlines the Editor (VAB/SPH) by hiding parts that have not yet been purchased in Career mode.

## Features

- **Cleaner Editor UI**: Automatically filters out parts in the VAB and SPH that are researched but not yet purchased.
- **Career Mode Focused**: Only active in Career mode. Sandbox and Science modes remain unaffected.
- **Difficulty Aware**: Respects the "Bypass Entry Purchase" difficulty setting. If enabled, all researched parts will be shown.
- **Native Implementation**: Uses KSP's built-in `EditorPartListFilter` system for maximum compatibility and performance.

## Installation

1. Download the latest release.
2. Extract the `HidePart` folder into your KSP `GameData` directory.
   - Path should look like: `[KSP Root]/GameData/HidePart/HidePart.dll`

## Requirements

- Kerbal Space Program 1.12.x (may work on older versions but untested).
- No external dependencies
## Credits

- Inspired by filtering logic in "Janitor's Closet".

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

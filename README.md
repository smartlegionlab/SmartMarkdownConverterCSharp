# Smart Markdown Converter (C#) <sup>v1.0.5</sup>

---

**Desktop application for bidirectional conversion between Markdown and HTML.**

---

[![GitHub top language](https://img.shields.io/github/languages/top/smartlegionlab/SmartMarkdownConverterCSharp)](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp)
[![GitHub license](https://img.shields.io/github/license/smartlegionlab/SmartMarkdownConverterCSharp)](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/blob/master/LICENSE)
[![GitHub release](https://img.shields.io/github/v/release/smartlegionlab/SmartMarkdownConverterCSharp)](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/)
[![GitHub stars](https://img.shields.io/github/stars/smartlegionlab/SmartMarkdownConverterCSharp?style=social)](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/smartlegionlab/SmartMarkdownConverterCSharp?style=social)](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/network/members)
![Platform](https://img.shields.io/badge/platform-windows-lightgrey)

---

### Main Interface

![Main Interface](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/raw/master/data/images/smart-markdown-converter-cs.png)

---

## Disclaimer

**By using this software, you agree to the full disclaimer terms.**

**Summary:** Software provided "AS IS" without warranty. You assume all risks.

**Full legal disclaimer:** See [DISCLAIMER.md](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/blob/master/DISCLAIMER.md)

---

## Features

- Bidirectional conversion: Markdown to HTML and HTML to Markdown
- Supports headings, bold, italic, links, code, code blocks, tables, and lists
- Dark theme interface
- Two-panel layout: input on the left, result on the right
- Copy result to clipboard with one click
- Open and save `.md`, `.markdown`, `.html`, `.htm`, `.txt` files
- Keyboard shortcuts
- Create desktop shortcut from the app
- Character counter
- Custom dark themed dialogs

## Key Shortcuts

| Shortcut           | Action                     |
|--------------------|----------------------------|
| `Ctrl + Enter`     | Convert                    |
| `Ctrl + Shift + C` | Copy result                |
| `Ctrl + N`         | Clear all fields           |
| `Ctrl + O`         | Open file                  |
| `Ctrl + S`         | Save file                  |
| `Ctrl + H`         | Show hotkeys               |
| `Ctrl + Shift + S` | Create desktop shortcut    |
| `Alt + F4`         | Exit application           |

## Download

### Pre-built Binary

| Platform        | Download                                                                                                                   |
|-----------------|----------------------------------------------------------------------------------------------------------------------------|
| **Windows x64** | [smart-markdown-converter-windows-x64.exe](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/releases/latest) |

> Just download, run, and start using. No .NET installation required.

## Quick Start

### Run the Application

```cmd
smart-markdown-converter-windows-x64.exe
```

### Usage

1. Select direction in the **Direction** dropdown: `Markdown -> HTML` or `HTML -> Markdown`
2. Paste or type your text in the **Input** panel
3. Click **Convert** or press `Ctrl + Enter`
4. Result appears in the **Result** panel
5. Click **Copy** or press `Ctrl + Shift + C` to copy the result

### Open and Save Files

- **Open** (`Ctrl + O`): load `.md`, `.markdown`, `.html`, `.htm`, `.txt` files
- **Save** (`Ctrl + S`): save result as `.md`, `.html`, `.txt`

### Create Desktop Shortcut

Use **Tools -> Create Desktop Shortcut** or press `Ctrl + Shift + S` to place a shortcut on your desktop.

## System Requirements

- **Operating System:** Windows 10 / 11 (x64)
- No .NET installation required (self-contained build)

## Build from Source

```bash
git clone https://github.com/smartlegionlab/SmartMarkdownConverterCSharp.git
cd SmartMarkdownConverterCSharp
dotnet build
```

For release build:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish
```

## License

**[BSD 3-Clause License](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/blob/master/LICENSE)**

Copyright (©) 2026, [Alexander Suvorov](https://github.com/smartlegionlab)

## Author

**Alexander Suvorov** - [GitHub](https://github.com/smartlegionlab)

---

## Support

- **Issues**: [GitHub Issues](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/issues)
- **Documentation**: This [README](https://github.com/smartlegionlab/SmartMarkdownConverterCSharp/blob/master/README.md)


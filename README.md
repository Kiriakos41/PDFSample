# PDFSample - .NET MAUI PDF Generator

A simple **.NET MAUI** application that generates **PDF files** using **iText7**.  
Supports **Greek, English, and other languages** depending on the font you use.

---

## Features

- Generate PDF files dynamically with **custom fonts**.
- Supports **Greek**, **English**, and **multi-language** text.
- Add **bold** and **regular** text styles.
- Copies PDF to **Downloads** folder on Android.
- Opens PDF using the device's default PDF viewer.

---

## Supported Fonts & Languages

| Font                  | Languages Supported       | Example Text                           |
|----------------------|-------------------------|---------------------------------------|
| OpenSans-Regular      | Greek, English, Unicode | Παράδειγμα ελληνικών / Example English |
| OpenSans-Semibold     | Greek, English, Unicode | Έξοδα και έσοδα / Expenses & Income   |
| Roboto-Regular        | English, Multilingual   | Hello, world! / Γειά σου κόσμε!       |
| NotoSans-Regular      | Multilingual            | こんにちは世界 / Привет мир / Γειά!   |

> You can replace fonts by copying them into the app package and referencing them in code.

---

## Getting Started

### Prerequisites

- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/overview/) environment.
- iText7 NuGet packages:
  ```bash
  dotnet add package itext7

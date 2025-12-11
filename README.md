# backend_welcome_message

## Description

This project will be a simple CLI program that gives users a welcome message customized to change based on the current time and date.

## Flowchart (overview)

```mermaid
flowchart TD
    A[Start] --> B[Read current time (DateTime.Now)]
    B --> C{--at argument provided?}
    C -- yes --> D[Parse provided time]
    C -- no --> E[Use current time]
    D --> F[Prompt for user name]
    E --> F
    F --> G{Name empty?}
    G -- yes --> H[Set name to Guest]
    G -- no --> I[Keep provided name]
    H --> J[Determine time-of-day bucket via switch]
    I --> J
    J --> K[Determine day message via DayOfWeek switch]
    K --> L[Format date/time (dd.MM.yyyy HH:mm)]
    L --> M[Build Spectre.Console table]
    M --> N[Output table]
    N --> O[End]
```

## Dev environment (Nix flake)

- Prerequisite: Nix with flakes enabled.
- Enter the shell:

```sh
nix develop
```

- Run the app:

```sh
dotnet run
```

- Or run via flake app:

```sh
nix run
```

## Without Nix

- Install .NET SDK 8.
- Run:

```sh
dotnet run
```

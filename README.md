# MAKING A DISCORD BOT IN C#

<p align="center">
  <img src="./examples/logo.png" width="100px" height="100px" />
</p>

## Features

- Commands
- Embed Messages

## Setups

- setup bot and generate token from [discord developer portal](https://discord.com/developers/docs/intro)
- create `config.json` at root

```json
{
  "token": "your-token-here",
  "prefix": "your-prefix-here"
}
```

- send messages/commands with defiend prefix (example `!embed`)
- can see the commands in `TestCommands.cs`

## Scripts

### Watch Run

```bash
dotnet watch run
```

### Build Run

```bash
dotnet run
```

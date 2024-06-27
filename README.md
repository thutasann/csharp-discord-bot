# MAKING A DISCORD BOT IN C# 🤖

Making the Discord Bot in C# with [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus).

<p align="center">
  <img src="./examples/logo.png" width="100px" height="100px" />
</p>

## Features

- Commands
- Embed Messages
- Mini CardGame Embed Message
- Poll and Reactions Embed Message
- Commands Attributes and Events Handler
- Slash Commands
- Group Slash Commands
- Interaction Components (Buttons, Dropdown, Inputs, Modals)

## Setups

- setup bot and generate token from [discord developer portal](https://discord.com/developers/docs/intro)
- create `config.json` at root

```json
{
  "token": "your-token-here",
  "prefix": "your-prefix-here"
}
```

- send messages/commands with defined prefix (example `!embed`, `!cardgame`)
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

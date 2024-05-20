# PersonalWelcomeMessage
Personal Welcome Message plugin for Rust servers using Oxide.

# Introduction

The PersonalWelcomeMessage plugin sends a private welcome message to players when they join the server. It supports separate messages for first-time and returning players, customizable delays before sending the message, and placeholders for the player's name and the server's name.

# Features
Sends a private message to players when they connect to the server.
Different messages for first-time and returning players.
Configurable delay before sending the message.
Supports {player_name} and {server_name} placeholders in the message for personalization.

# Configuration
The plugin uses a JSON configuration file called "WelcomeMessages.json" to store its settings. The configuration options include:

Welcome Message: The message to be sent to returning players.

First Time Player Message: The message to be sent to first-time players.

Delay Before Sending Message (seconds): The number of seconds to wait before sending the message.

Both the Welcome Message and First Time Player Message options support the {player_name} and {server_name} placeholders, which will be replaced with the player's name and the server's name, respectively, when the message is sent.

**Example Configuration:**

```
{  
  "Welcome Message": "Hi {player\_name}! Welcome back to {server\_name}.",  
  "First Time Player Message": "Hi {player\_name}! Welcome to {server\_name}. Our ruleset is heavily enforced and bans are issued daily. Ignorance is not an excuse, please read the rules and understand them BEFORE you begin playing: example rule url | THANK YOU FOR PLAYING!",  
  "Delay Before Sending Message (seconds)": 5.0  
}
```

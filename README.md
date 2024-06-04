## Player activity
The mod saves a detailed history of player actions on the server.

The entire history is stored on the server in a common folder with _Valheim data_ in a separate folder named `Player Activity`.

If you are using a _hosting service_, then the history will be in the same folder as the folder with the server world.

Action history files are saved separately for each player and combined into folders by day.

**Important: considering that the time on the server may differ from the time on the client, the mod uses UTC time.**

The mod is required for installation on the client and server.

### Actions
The mod is currently monitoring the following actions
<details><summary>Click to expand</summary>

| _Action_    |                                      _When it appears_                                      |                                      _Parameters_                                      |
|-----------------|:-------------------------------------------------------------------------------------------:|:--------------------------------------------------------------------------------------:|
| Spawned         |                                 Player spawned in the world                                 |                        player name, player id, player position                         |
| Inventory       |                          Player spawned and then every 10 minutes                           |                items data (name, quality, count etc.), player position                 |
| Equip, Unequip  |                                Putting on/taking off an item                                |                               item data, player position                               |
| Consume         |                                   Consumption of an item                                    |                       inventory name, item data, player position                       |
| Craft           |                                      Crafting an item                                       |                               item data, player position                               |
| Repair item     |                             Repairing an item in the inventory                              |                               item data, player position                               |
| Dodge           |                                           Evasion                                           |                                    player position                                     |
| Teleport        |                                        Teleportation                                        |                            target position, player position                            |
| Damage          |        Dealing damage to a player, creature, building, or other destructible object         |                       target name, damage value, target position                       |
| Damaged         |                                 Taking damage by the player                                 |                              attacker name, damage value, player position              |
| Dead            |                                       Player is dead                                        |                                     attacker name, player position                     |
| Pickup          |                             Collecting an item from the ground                              |                                item data, item position                                |
| Drop            |                           Throwing an item out of inventory/chest                           |                       inventory name, item data, player position                       |
| Place           |                           Creating a building (or terrain change)                           |                            building name, building position                            |
| Remove          |                                     Deleting a building                                     |                            building name, building position                            |
| Repair building |                                   Repair of the building                                    |                            building name, building position                            |
| Interact        |            Interaction with an interactive object (door, ward, chest, bed, etc.)            | object name, object position, optional information (ex. name of the owner of the ward) |
| Use             | Using an item from the inventory with an object in the world (ex. adding fuel to the stove) |                        object name, item data, object position                         |
| Text            |                      Changing the text (animal name, portal tag, etc.)                      |                         object name, new text, object position                         |
| MoveAll         |                            Move the entire inventory to another                             |                inventory names (from, to), items data, player position                 |
| Move            |                        Moving an item from one inventory to another                         |                 inventory names (from, to), item data, player position                 |
| Grave           |                      The player's grave has been created                                    |             items data in grave, items data in player inventory, grave position        |
| Ping            |                                      Once a minute                                          |                              ping value, player position                               |
| Connected       |                               Player connected to server                                    |                          none                                                          |
| Disconnected    |                               Player disconnected from server                               |                          player position (if player alive)                             |
| Command         |                          Calling a command in the console or chat                           |                             command text, player position                              |
| Command remote  |                        Calling server command in the console or chat                        |                             command text, player position                              |

</details>

#### If you have any questions / bug reports / suggestions for improvement or found incompatibility with another mod, feel free to contact me in discord `typedeff`

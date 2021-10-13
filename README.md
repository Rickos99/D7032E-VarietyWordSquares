# D7032E-VarietyWordSquares
Home exam for course D7032E Software engineering. Tasked with refactoring source code for a Word Square game

# Building
1. Install the X64 version of [.NET 5.0 (or higher) SDK](https://dotnet.microsoft.com/download/dotnet)


# Usage

# Rules and requirements:
1. Game participants consists of at least one player and any number of bots.
2. The game-board is a grid with at least one row and at least one column.
3. Random participant starts picking the letter to place on the board, then participants take turns picking.
4. All participants place the picked letter somewhere on their personal game-board.
5. When the game-board is fully populated with letters the score is calculated for each player.
6. Words are identified horizontally, vertically, and forward diagonally.
7. A scrabble dictionary is used for the selected language to identify words.
8. For standard WordSquares scoring the following rules apply:
	  1. Words consisting of fewer than 3 letters = 0 points
	  2. Words consisting of 3 letters = 1 points
	  3. Words with more than 3 letters: (word-length-3) * 2 points
	  4. A word can only be counted once when calculating the score.
	  5. If a word is used more than once only the highest scoring instance is counted.
9. For scrabble WordSquares scoring the following rules apply:
	  1. Letters are assigned a value according to the Scrabble rules for the used language. https://en.wikipedia.org/wiki/Scrabble_letter_distributions
    2. The Double Letter tile doubles the value of the letter placed there
    3. The Triple Letter tile triples the value of the letter placed there
    4. The Double Word tile doubles the value of all words formed using the tile (after letter-multipliers are applied).
	  5. The Triple Word tile triples the value of all words formed using the tile (after letter-multipliers are applied).
	  6. Multiple word-multipliers used in a word are multiplied together.
	  7. Tiles with no modifier are regular tiles and have a multiplier of 1 (e.g. Scrabble WordSquares on regular board)
10. The player with the highest point total is announced as the winner at the end of each match.
11. Scrabble boards where specified special tiles are either pre-placed or randomised can be loaded from the menu.
12. Supported languages have their own letter-points configurations.

#### Future modifications to the original game
13. New game-mode: Boggle Word Squares
    1. Instead of placing letters on a personal board players take turns picking and placing the letter on a shared board.
    2. When all letters are placed, players now compete by finding words in the grid themselves within a time-limit.
    3. The time-limit for the finding the word can be configured in the menu but is set to 60 seconds by default.
    4. Bots can use the algorithm (checkWords) which finds all possible words (no need to program a special AI).
    5. This game-mode can use both regular and scrabble-boards, and the score is calculated according to rule 8 & 9.
14. New game-mode: Don’t make a word
    1. Players take turns picking letters, but the next player in line places the letter on a shared board.
    2. Players must not form a word longer than two letters in any of the directions specified in rule 6.
    3. A player loses when they form a word longer than two letters while placing their assigned letter.
    4. The game ends when only one player remains (the winner) or when there is no more room for letters on the board (remaining players are then shared winners).
15. Add a way for players to define and load new scrabble-boards with either pre-placed or randomised scrabble-tiles.
16. Add support for additional languages (in addition to English), including dictionary and letter-points configurations.
17. Add support for showing the list of point-scoring words and associated points at the end of the game for each player which can be enabled from the settings menu.

#### Additional requirements (in addition to rules 1-17)
18. It should be easy to modify and extend your software (modifiability and extensibility quality attributes). It is to support future modifications such as the ones proposed in the “future modifications to the game” section (you don’t need to implement these unless you want to, just structure the architecture so it is easy to do in the future. Though implementing some of the future modifications may help you see whether your updated structure is good or not).
19. It should be easy to test, and to some extent debug, your software (testability quality attribute). This is to be able to test the game rules as well as different phases of the game-play.

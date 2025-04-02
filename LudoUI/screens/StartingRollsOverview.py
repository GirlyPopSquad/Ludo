import pygame

from Constants import WIDTH, WHITE, HEIGHT, HOT_PINK, DEEP_PINK
from clients.StartingRollClient import get_should_reroll
from draw.button import init_standard_button
from draw.dice import draw_dice
from draw.ludo_piece import draw_ludo_piece
from models.Lobby import Lobby
from models.LobbyPlayer import LobbyPlayer
from models.Roll import Roll
from stateManagers.GameStateManager import quit_game, get_game_state, GameState, set_game_state
from stateManagers.LobbyStateManager import get_lobby, set_lobby


def starting_rolls_overview(screen, font):
    pygame.display.set_caption("Ludo - Starting Rolls Overview")
    screen.fill(WHITE)

    for i in range(1, len(get_lobby().players) + 1):
        piece_position = WIDTH // 5 * i
        dice_position = WIDTH // 5 * i

        draw_ludo_piece(screen, piece_position, 180, get_lobby().players[i - 1].id, font)
        draw_dice(screen, 40, dice_position, 180, get_lobby().rolls[i - 1].value, font)

    has_to_reroll = get_should_reroll(get_lobby().rolls)

    if has_to_reroll:
        button = setup_reroll_button()
    else:
        button = setup_start_game_button()

    button.draw(screen, font)

    while get_game_state() == GameState.LOBBY:

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                button.check_click()

        pygame.display.update()


def setup_reroll_button():
    return init_standard_button("Do Reroll", HOT_PINK, DEEP_PINK, on_reroll)


# TODO: implement
def on_reroll():
    print("ON REROLL")
    set_game_state(GameState.NOT_IMPLEMENTED)


def setup_start_game_button():
    return init_standard_button("Start Game", HOT_PINK, DEEP_PINK, on_reroll)

# TODO: implement
def on_start_game():
    print("ON START")
    set_game_state(GameState.NOT_IMPLEMENTED)

def test():
    pygame.init()

    # Set up display
    screen = pygame.display.set_mode((WIDTH, HEIGHT))

    font = pygame.font.Font(None, 50)

    roll_values = [2, 4, 6, 6]

    starting_players: [LobbyPlayer] = []
    starting_rolls: [Roll] = []
    for j in range(1, 5):
        player = LobbyPlayer(j)
        roll = Roll(player, roll_values[j - 1])

        starting_players.append(player)
        starting_rolls.append(roll)

    test_lobby = Lobby(1, starting_players, starting_rolls)

    set_lobby(test_lobby)

    starting_rolls_overview(screen, font)

    game_on = True

    while game_on:
        # Event handling
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                game_on = False
                quit_game()

        pygame.display.update()

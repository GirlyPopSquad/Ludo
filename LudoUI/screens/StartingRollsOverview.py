import pygame

from Constants import WIDTH, WHITE, HEIGHT, HOT_PINK, DEEP_PINK
from clients.StartingRollClient import get_should_reroll
from draw.button import init_standard_button
from draw.dice import draw_dice
from draw.ludo_piece import draw_ludo_piece
from models.Lobby import Lobby
from models.LobbyPlayer import LobbyPlayer
from models.Roll import Roll
from screens.StartingRerolls import starting_rerolls
from stateManagers.GameStateManager import quit_game, get_game_state, GameState, set_game_state
from stateManagers.LobbyStateManager import LobbyState, get_lobby, get_lobby_state, set_lobby, set_lobby_state


def starting_rolls_overview(screen, font):
    pygame.display.set_caption("Ludo - Starting Rolls Overview")
    screen.fill(WHITE)

    for i in range(1, len(get_lobby().players) + 1):
        piece_position = WIDTH // 5 * i
        dice_position = WIDTH // 5 * i

        draw_ludo_piece(screen, piece_position, 180, get_lobby().players[i - 1].id, font)
        draw_dice(screen, 40, dice_position, 180, get_lobby().rolls[i - 1].value, font)

    def on_reroll():
        set_lobby_state(LobbyState.STARTING_REROLL)

    def setup_reroll_button():
        return init_standard_button("Do Reroll", HOT_PINK, DEEP_PINK, on_reroll)

    has_to_reroll = get_should_reroll(get_lobby().rolls)

    if has_to_reroll:
        button = setup_reroll_button()
    else:
        button = setup_start_game_button()

    button.draw(screen, font)

    while get_lobby_state() == LobbyState.ROLLS_OVERVIEW and get_game_state() == GameState.LOBBY:

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                button.check_click()

        pygame.display.update()

def setup_start_game_button():
    return init_standard_button("Start Game", HOT_PINK, DEEP_PINK, on_start_game)

# TODO: implement
def on_start_game():
    print("ON START")
    set_game_state(GameState.NOT_IMPLEMENTED)
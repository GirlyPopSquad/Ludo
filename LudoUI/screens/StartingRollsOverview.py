import pygame

import clients.GameClient as gameClient
from Constants import WIDTH, WHITE, HOT_PINK, DEEP_PINK
from clients.LobbyClient import get_lobby
from clients.StartingRollClient import get_should_reroll
from draw.button import init_standard_button
from draw.dice import draw_dice
from draw.ludo_piece import draw_ludo_piece
from stateManagers.GameStateManager import quit_game, get_game_state, GameState, set_game_id
from stateManagers.IsPygameRunning import set_is_pygame_running, get_is_pygame_running
from stateManagers.LobbyStateManager import LobbyState, get_lobby_id, get_lobby_state, set_lobby_state


def starting_rolls_overview(screen, font):
    pygame.display.set_caption("Ludo - Starting Rolls Overview")
    screen.fill(WHITE)

    lobby_id = get_lobby_id()
    looby = get_lobby(lobby_id)

    has_to_reroll = get_should_reroll(looby.rolls)

    #todo: this should come from the backend
    highest_roll = max(looby.rolls, key=lambda r: r.value)
    winner_id = highest_roll.player_id

    for i in range(1, len(looby.rolls) + 1):
        piece_position = WIDTH // 5 * i
        dice_position = WIDTH // 5 * i
        roll = looby.rolls[i - 1]

        if has_to_reroll:
            draw_ludo_piece(screen, piece_position, 180, roll.player_id, font)
        else:
            is_winner = roll.player_id == winner_id
            draw_ludo_piece(screen, piece_position, 180, roll.player_id, font, is_winner)
        
        draw_dice(screen, 40, dice_position, 180, roll.value, font)

    def on_reroll():
        set_lobby_state(LobbyState.STARTING_REROLL)

    def setup_reroll_button():
        return init_standard_button("Do Reroll", HOT_PINK, DEEP_PINK, on_reroll)

    has_to_reroll = get_should_reroll(looby.rolls)

    if has_to_reroll:
        button = setup_reroll_button()
    else:
        button = setup_start_game_button()

    button.draw(screen, font)

    while get_lobby_state() == LobbyState.ROLLS_OVERVIEW and get_game_state() == GameState.LOBBY:
        if get_is_pygame_running():
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    quit_game()
                elif event.type == pygame.MOUSEBUTTONDOWN:
                    button.check_click()
            if get_is_pygame_running():
                pygame.display.update()
            else:
                break

def setup_start_game_button():
    return init_standard_button("Start Game", HOT_PINK, DEEP_PINK, on_start_game)

def on_start_game():

    lobby_id = get_lobby_id()

    game_id = gameClient.create_game(lobby_id)

    set_game_id(game_id)

    set_is_pygame_running(False)
    pygame.quit()

    from game.BoardFromTiles import open_ludoboard_window
    open_ludoboard_window()
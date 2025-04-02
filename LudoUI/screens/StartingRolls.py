import pygame

from Constants import WHITE, WIDTH, BLACK, HEIGHT, DEEP_PINK
from PlayerColor import get_piece_colorcode
from clients.StartingRollClient import next_starting_roll
from draw.button import init_standard_button
from draw.dice import draw_dice
from draw.ludo_piece import draw_ludo_piece
from screens.StartingRollsOverview import starting_rolls_overview
from stateManagers.GameStateManager import GameState, get_game_state, quit_game
from stateManagers.LobbyStateManager import LobbyState, get_lobby, get_lobby_state, set_lobby, set_lobby_state


def starting_roll(screen, font):
    pygame.display.set_caption("Ludo - Starting Roll")

    starting_roll_frame(screen, font, 1, "?", "Roll", 0)


def starting_roll_frame(screen, font, player_id, dice_value, button_text, state):
    screen.fill(WHITE)
    text = font.render("Starting Roll", True, BLACK)
    screen.blit(text, (WIDTH // 2 - text.get_width() // 2, HEIGHT // 5 - text.get_height() // 2))
    draw_ludo_piece(screen, WIDTH // 5, 180, player_id, font)
    draw_dice(screen, 80, WIDTH // 5, 180, dice_value, font)

    player_color = get_piece_colorcode(player_id)

    def on_click_next():
        updated_player_id = player_id + 1
        lobby = get_lobby()
        if updated_player_id > len(lobby.players):
            starting_roll_frame(screen, font, player_id, dice_value, "Done", 3)

        else:
            starting_roll_frame(screen, font, updated_player_id, "?", "Roll", 0)

    def on_click_roll():
        updated_dice_value = on_starting_roll()
        updated_button_text = "Next"
        starting_roll_frame(screen, font, player_id, updated_dice_value, updated_button_text, 1)

    def on_done():
        set_lobby_state(LobbyState.ROLLS_OVERVIEW)

    button_action = None

    if state == 0:
        button_action = on_click_roll
    elif state == 1:
        button_action = on_click_next
    elif state == 3:
        button_action = on_done

    new_button = init_standard_button(button_text, player_color, DEEP_PINK, button_action)
    new_button.draw(screen, font)

    while get_lobby_state() == LobbyState.STARTING_ROLL and get_game_state() == GameState.LOBBY:

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                new_button.check_click()

        pygame.display.update()


def on_starting_roll():
    updated_lobby = next_starting_roll()
    set_lobby(updated_lobby)
    return updated_lobby.rolls[-1].value

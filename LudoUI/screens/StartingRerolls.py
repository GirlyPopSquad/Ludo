import pygame
from Constants import WHITE, WIDTH, HEIGHT, BLACK, DEEP_PINK, HOT_PINK
from clients.StartingRollClient import next_starting_roll, get_rerollers, handle_reroll
from draw.button import init_standard_button
from draw.dice import draw_dice
from draw.ludo_piece import draw_ludo_piece
from models.Lobby import Lobby
from models.LobbyPlayer import LobbyPlayer
from models.Roll import Roll
from stateManagers.GameStateManager import quit_game, get_game_state, GameState, set_game_state
from stateManagers.LobbyStateManager import get_lobby, set_lobby

def starting_rerolls(screen, font):
    pygame.display.set_caption("Ludo - Starting Rerolls")
    screen.fill(WHITE)

    reroll_players = get_rerollers(get_lobby().rolls)

    reroll_frame(screen, font, reroll_players[0])





def reroll_frame(screen, font, player):
    screen.fill(WHITE)

    def on_reroll():
        print("ON REROLL")
        reroll_value = handle_reroll(player)
        reroll_frame_value(screen, font, player, reroll_value)

    draw_ludo_piece(screen, WIDTH // 5, 180, player.id, font)
    draw_dice(screen, 80, WIDTH // 5, 180, "?", font)

    reroll_button = init_standard_button("Reroll", HOT_PINK, DEEP_PINK, on_reroll)
    reroll_button.draw(screen, font)

    while get_game_state() == GameState.LOBBY:

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                reroll_button.check_click()

        pygame.display.update()


def reroll_frame_value(screen, font, player, value):
    screen.fill(WHITE)

    def on_next():
        print("ON NEXT")

    draw_ludo_piece(screen, WIDTH // 5, 180, player.id, font)
    draw_dice(screen, 80, WIDTH // 5, 180, value, font)

    reroll_button = init_standard_button("Next", HOT_PINK, DEEP_PINK, on_next)
    reroll_button.draw(screen, font)

    while get_game_state() == GameState.LOBBY:

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                reroll_button.check_click()

        pygame.display.update()
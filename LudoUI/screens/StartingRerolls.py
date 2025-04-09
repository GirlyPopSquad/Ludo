import pygame

from Constants import WHITE, WIDTH, DEEP_PINK, HOT_PINK
from clients.StartingRollClient import get_rerollers, handle_reroll, remove_old_rolls
from draw.button import init_standard_button
from clients.LobbyClient import get_lobby
from draw.dice import draw_dice
from draw.ludo_piece import draw_ludo_piece
from stateManagers.GameStateManager import quit_game, get_game_state, GameState, set_game_state
from stateManagers.LobbyStateManager import LobbyState, get_lobby_id, get_lobby_state, set_lobby_state


def starting_rerolls(screen, font):
    pygame.display.set_caption("Ludo - Starting Rerolls")
    screen.fill(WHITE)

    lobby_id = get_lobby_id()
    lobby = get_lobby(lobby_id)
    reroll_players = get_rerollers(lobby.rolls)
    remove_old_rolls(lobby.lobby_id, reroll_players)    
    
    reroll_frame(screen, font, 0, reroll_players)
    
    set_lobby_state(LobbyState.ROLLS_OVERVIEW)

def reroll_frame(screen, font, index, reroll_players):
    screen.fill(WHITE)
    
    player = reroll_players[index]

    def on_reroll():
        reroll_value = handle_reroll(player)
        reroll_frame_value(screen, font, index, reroll_players, reroll_value)

    draw_ludo_piece(screen, WIDTH // 5, 180, player.id, font)
    draw_dice(screen, 80, WIDTH // 5, 180, "?", font)

    reroll_button = init_standard_button("Reroll", HOT_PINK, DEEP_PINK, on_reroll)
    reroll_button.draw(screen, font)

    while get_lobby_state() == LobbyState.STARTING_REROLL and get_game_state() == GameState.LOBBY:

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                reroll_button.check_click()

        pygame.display.update()


def reroll_frame_value(screen, font, index, reroll_players, value):
    screen.fill(WHITE)        
    
    player = reroll_players[index]
        
    def on_next():                
        if index == len(reroll_players)-1:
            set_lobby_state(LobbyState.ROLLS_OVERVIEW)
        else:
            reroll_frame(screen, font, index+1, reroll_players)

    draw_ludo_piece(screen, WIDTH // 5, 180, player.id, font)
    draw_dice(screen, 80, WIDTH // 5, 180, value, font)

    reroll_button = init_standard_button("Next", HOT_PINK, DEEP_PINK, on_next)
    reroll_button.draw(screen, font)

    while get_lobby_state() == LobbyState.STARTING_REROLL and get_game_state() == GameState.LOBBY:

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                reroll_button.check_click()

        pygame.display.update()
                

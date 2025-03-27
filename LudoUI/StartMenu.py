import pygame

from Constants import WHITE, WIDTH, BLACK
from GameState import set_game_state, quit_game, GameState
from draw.button import init_play_button, init_quit_button
from draw.ludo_piece import draw_ludo_piece

def start_menu(screen, font):
    screen.fill(WHITE)

    title_text = font.render("Ludo Game", True, BLACK)
    screen.blit(title_text, (WIDTH // 2 - title_text.get_width() // 2, 50))

    piece_positions = [WIDTH // 5, WIDTH // 5 * 2, WIDTH // 5 * 3, WIDTH // 5 * 4]

    draw_ludo_piece(screen, piece_positions[0], 180, 1, font)
    draw_ludo_piece(screen, piece_positions[1], 180, 2, font)
    draw_ludo_piece(screen, piece_positions[2], 180, 3, font)
    draw_ludo_piece(screen, piece_positions[3], 180, 4, font)

    # Draw buttons
    play_button = init_play_button(start_game)
    play_button.draw(screen, font)

    quit_button = init_quit_button(quit_game)
    quit_button.draw(screen, font)

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            quit_game()
        elif event.type == pygame.MOUSEBUTTONDOWN:
            play_button.check_click()
            quit_button.check_click()


def start_game():
    set_game_state(GameState.LOBBY)

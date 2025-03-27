import sys

import pygame
import pygame.freetype

from Constants import WHITE, BLACK, WIDTH, HEIGHT, DEEP_PINK
from PlayerColor import get_piece_colorcode
from clients.LobbyClient import create_lobby
from clients.StartingRollClient import next_starting_roll, get_rerollers, get_should_reroll
from draw.button import init_standard_button, init_play_button, init_quit_button
from draw.dice import draw_dice
from draw.ludo_piece import draw_ludo_piece

# Initialize Pygame
pygame.init()

game_state = "StartMenu"

lobby = create_lobby()

# Set up display
screen = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("Ludo - Start Menu")

font = pygame.font.Font(None, 50)

# Functions for button actions
def start_game():
    global game_state
    game_state = "Lobby"

def on_starting_roll():
    global lobby
    updated_lobby = next_starting_roll(lobby)
    lobby = updated_lobby
    return updated_lobby.rolls[-1].value

def starting_roll():
    pygame.display.set_caption("Ludo - Starting Roll")

    starting_roll_frame(screen, 1, "?", "Roll", 0)


def starting_roll_frame(new_screen, player_id, dice_value, button_text, state):
    new_screen.fill(WHITE)
    text = font.render("Starting Roll", True, BLACK)
    new_screen.blit(text, (WIDTH // 2 - text.get_width() // 2, HEIGHT // 5 - text.get_height() // 2))
    draw_ludo_piece(new_screen, WIDTH // 5, 180, player_id, font)
    draw_dice(new_screen, WIDTH // 5, 180, dice_value, font)

    player_color = get_piece_colorcode(player_id)

    def on_click_next():
        updated_player_id = player_id + 1
        if updated_player_id > len(lobby.players):
            starting_roll_frame(new_screen, player_id, dice_value, "Done", 3)

        else:
            starting_roll_frame(new_screen, updated_player_id, "?", "Roll", 0)

    def on_click_roll():
        updated_dice_value = on_starting_roll()
        updated_button_text = "Next"
        starting_roll_frame(new_screen, player_id, updated_dice_value, updated_button_text, 1)

    def on_done():
        print("ON DONE")

        global game_state
        game_state = "NOT IMPLEMENTED"

        #TODO: See rolls and check for rerolls
        startingRolls = lobby.rolls
        should_we_reroll = get_should_reroll(startingRolls)
        if(should_we_reroll):
            rerollers = get_rerollers(startingRolls)


    button_action = None

    if state == 0:
        button_action = on_click_roll
    elif state == 1:
        button_action = on_click_next
    elif state == 3:
        button_action = on_done

    new_button = init_standard_button(button_text, player_color, DEEP_PINK, button_action)

    global game_state

    while game_state == "Lobby":

        new_button.draw(new_screen, font)

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                new_button.check_click()

        pygame.display.update()


def quit_game():
    pygame.quit()
    sys.exit()


def start_menu():
    gameOn = True

    while gameOn:
        screen.fill(WHITE)

        # Display title
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

        # Event handling
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                gameOn = False
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                play_button.check_click()
                quit_button.check_click()

        if game_state == "Lobby":
            starting_roll()

        if game_state == "NOT IMPLEMENTED":
            screen.fill(WHITE)
            not_implemented_text = font.render("Not implemented yet", True, BLACK)
            screen.blit(not_implemented_text, (
            WIDTH // 2 - not_implemented_text.get_width() // 2, HEIGHT // 2 - not_implemented_text.get_height() // 2))


        pygame.display.update()


# Run the start menu
start_menu()

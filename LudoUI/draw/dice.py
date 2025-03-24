import pygame

from Constants import WHITE, BLACK


def draw_dice(screen, x, y, dice_value, font):
    body_height = 50
    body_width = 50

    # Draw dice next to the Ludo piece
    dice_size = 80
    dice_x = x + body_width // 2 + 10
    dice_y = y - body_height // 2

    pygame.draw.rect(screen, WHITE, (dice_x, dice_y, dice_size, dice_size))
    pygame.draw.rect(screen, BLACK, (dice_x, dice_y, dice_size, dice_size), 2)

    # Draw dots on the dice based on the dice_value
    if dice_value == '?':
        question_mark = font.render('?', True, BLACK)
        question_mark_rect = question_mark.get_rect(center=(dice_x + dice_size // 2, dice_y + dice_size // 2))
        screen.blit(question_mark, question_mark_rect)
    else:
        # Draw dots on the dice based on the dice_value
        dot_radius = 6
        dot_positions = {
            1: [(dice_x + dice_size // 2, dice_y + dice_size // 2)],
            2: [(dice_x + dice_size // 4, dice_y + dice_size // 4),
                (dice_x + 3 * dice_size // 4, dice_y + 3 * dice_size // 4)],
            3: [(dice_x + dice_size // 4, dice_y + dice_size // 4), (dice_x + dice_size // 2, dice_y + dice_size // 2),
                (dice_x + 3 * dice_size // 4, dice_y + 3 * dice_size // 4)],
            4: [(dice_x + dice_size // 4, dice_y + dice_size // 4),
                (dice_x + 3 * dice_size // 4, dice_y + dice_size // 4),
                (dice_x + dice_size // 4, dice_y + 3 * dice_size // 4),
                (dice_x + 3 * dice_size // 4, dice_y + 3 * dice_size // 4)],
            5: [(dice_x + dice_size // 4, dice_y + dice_size // 4),
                (dice_x + 3 * dice_size // 4, dice_y + dice_size // 4),
                (dice_x + dice_size // 2, dice_y + dice_size // 2),
                (dice_x + dice_size // 4, dice_y + 3 * dice_size // 4),
                (dice_x + 3 * dice_size // 4, dice_y + 3 * dice_size // 4)],
            6: [(dice_x + dice_size // 4, dice_y + dice_size // 4),
                (dice_x + 3 * dice_size // 4, dice_y + dice_size // 4),
                (dice_x + dice_size // 4, dice_y + dice_size // 2),
                (dice_x + 3 * dice_size // 4, dice_y + dice_size // 2),
                (dice_x + dice_size // 4, dice_y + 3 * dice_size // 4),
                (dice_x + 3 * dice_size // 4, dice_y + 3 * dice_size // 4)]
        }

        for pos in dot_positions[dice_value]:
            pygame.draw.circle(screen, BLACK, pos, dot_radius)

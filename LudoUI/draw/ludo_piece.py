import pygame

from Constants import BLACK
from PlayerColor import get_piece_colorcode


def draw_crown(surface, x, y):
    crown_color = (255, 215, 0)  # Gold
    crown_width = 24
    crown_height = 18

    # Løft kronen højere op (øget fra - crown_height til - crown_height - 10)
    top_offset = 10
    left = x - crown_width // 2
    top = y - crown_height - top_offset

    points = [
        (left, top + crown_height),
        (left + 5, top + 5),
        (left + 10, top + crown_height),
        (left + 14, top + 5),
        (left + 19, top + crown_height),
        (left + 24, top + 5),
        (left + crown_width, top + crown_height),
    ]

    pygame.draw.polygon(surface, crown_color, points)

    # Farvede "diamanter"
    pygame.draw.circle(surface, (255, 0, 0), (left + 5, top + 5), 2)
    pygame.draw.circle(surface, (0, 255, 0), (left + 14, top + 5), 2)
    pygame.draw.circle(surface, (0, 0, 255), (left + 24, top + 5), 2)


def draw_ludo_piece(surface, x, y, player_id, font, is_winner=False):
    body_height = 50
    body_width = 50
    head_radius = 15
    head_offset = 10
    color = get_piece_colorcode(player_id)

    outline_color = (255, 215, 0) if is_winner else BLACK  # Gold outline if winner

    # Draw black or gold outline for body and head
    pygame.draw.polygon(surface, outline_color, [(x - body_width // 2 - 2, y),
                                                 (x + body_width // 2 + 2, y),
                                                 (x, y - body_height - 2)])
    pygame.draw.circle(surface, outline_color, (x, y - body_height - head_radius + head_offset), head_radius + 2)

    # Draw colored body and head
    pygame.draw.polygon(surface, color, [(x - body_width // 2, y),
                                         (x + body_width // 2, y),
                                         (x, y - body_height)])
    pygame.draw.circle(surface, color, (x, y - body_height - head_radius + head_offset), head_radius)

    # Draw crown if this piece is the winner
    if is_winner:
        draw_crown(surface, x, y - body_height - 10)

    # Player number under the piece
    piece_text = font.render(str(player_id), True, BLACK)
    text_rect = piece_text.get_rect(center=(x, y + 30))
    surface.blit(piece_text, text_rect)
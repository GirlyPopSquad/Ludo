from models.Roll import Roll
from clients.StartingRollClient import next_starting_roll, get_should_reroll, get_rerollers

# todo: ?
def test_starting_roll_client():
    rolls = Roll.generate_test_rolls()
    should_we_reroll = get_should_reroll(rolls)
    if should_we_reroll:
        rerollers = get_rerollers(rolls)
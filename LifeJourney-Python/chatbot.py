from typing import Dict

answer_data: Dict[str, str] = {
    "hi": "hello!",
    "hello": "hello",
    "how are you": "I'm doing well, thank you!",
    "bye": "Goodbye!",
    "what can you do": "I can answer your questions in an informative way, following your instructions, and completing your requests thoughtfully. How can I help you today?",
    
}

def handle_greeting_farewell(message):
    greetings = ["hi", "hello", "hey", "good morning", "good afternoon", "good evening"]
    farewells = ["bye", "goodbye", "see you later", "talk to you soon"]

    if message.text.lower() in greetings:
        return answer_data.get("hi")
    elif message.text.lower() in farewells:
        return answer_data.get("bye")
    else:
        return None
import re

def handle_complex_patterns(message):
    weather_pattern = r"weather in (.*)"
    search_pattern = r"search for (.*)"

    match = re.search(weather_pattern, message.text.lower())
    if match:
        location = match.group(1)
        weather_response = f"Here's the weather information for {location} (replace with actual weather data)"
        return weather_response

    match = re.search(search_pattern, message.text.lower())
    if match:
        query = match.group(1)
        search_response = f"Search results for '{query}' (replace with actual search results)"
        return search_response

    return None

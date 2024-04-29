from fastapi import FastAPI
from pydantic import BaseModel
from chatbot import handle_complex_patterns, handle_greeting_farewell, answer_data

app = FastAPI()

class Message(BaseModel):
    text: str

@app.post("/chat")
async def chat(message: Message):
    user_message = message.text.lower()

    greeting_farewell_response = handle_greeting_farewell(message)
    if greeting_farewell_response:
        return {"message": greeting_farewell_response}

    complex_response = handle_complex_patterns(message)
    if complex_response:
        return {"message": complex_response}

    response = answer_data.get(user_message)

    if not response:
        try:
            from nltk.corpus import wordnet
            from nltk.metrics import edit_distance

            best_match = None
            min_distance = float('inf')
            for word in answer_data.keys():
                distance = edit_distance(user_message, word)
                if distance < min_distance:
                    min_distance = distance
                    best_match = word
        except ModuleNotFoundError:
            pass

        if best_match:
            response = answer_data.get(best_match)
        else:
            response = answer_data.get("default")

    return {"message": response}

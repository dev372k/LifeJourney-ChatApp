const myToast = Toastify({
    text: "New message",
    duration: 5000
})

const html = `<div class="mx-2 {3}">
                <p><strong>{0}</strong> - {1}</p>
                <p>{2}</p>
            </div>`

const reply = (message) => {
    const response = JSON.parse(message)
    var newHtml = ""
    if (response.IsBot) {
        newHtml = format(html, response.Username, response.CreatedOn.toString(), response.Text, "message left")

        myToast.showToast();
    }
    else
        newHtml = format(html, response.Username, response.CreatedOn.toString(), response.Text, "message right")

    $("#chat-box").append(newHtml)
    toBottom()
}

const format = (template, ...values) => {
    return template.replace(/{(\d+)}/g, (match, index) => {
        return values[index] !== undefined ? values[index] : match;
    });
}

const toBottom = () => {
    let specificDiv = $("#chat-box");
    specificDiv.scrollTop(specificDiv.prop("scrollHeight"));
}
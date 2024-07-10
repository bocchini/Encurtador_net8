document.getElementById('btnSubmit')
.addEventListener('click', function (e) {
    e.preventDefault();

    handleSubmitAsync();
});

function handleSubmitAsync() {
    const url = document.getElementById('url').value;

    var json = { 'url': url }

    const headers = {
        'content-type': 'application/json'
    };

    fetch('/urls', { method: 'post', body: JSON.stringify(json), headers })
        .then(apiResult => apiResult.json())
        .then(json => {
            console.log(json);

            document.getElementById('urlResult').innerHTML = json.shortUrl;
        })
}

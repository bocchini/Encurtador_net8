let btnSubmit = document.getElementById('btnSubmit');
let buttonCopy = document.getElementById('execCopy');
let url = document.getElementById('url');

buttonCopy.style.display = 'none';

buttonCopy.addEventListener('click', execCopy, false);
btnSubmit.addEventListener('click', handleSubmitAsync, false);

url
    .addEventListener('keyup', function (evt) {
    if (evt.code === 'Enter')
    sendLink();
})

function handleSubmitAsync(e) {
    e.preventDefault();
    let link = url.value;
    if (link) sendLink();    
}

function sendLink() {
    var json = { 'url': url.value };

    const headers = {
        'content-type': 'application/json'
    };

    fetch('/urls', { method: 'post', body: JSON.stringify(json), headers })
        .then(apiResult => {
            return new Promise(resolve => apiResult.json()
                .then(json => {
                    return resolve({ ok: apiResult.ok, status: apiResult.status, json: json })
                })
            );
        })
        
        .then(({json, ok, status}) => {
            console.log(json);
            if (ok) {
                const anchor = `<a href=${json.shortUrl} target="_blank">${json.shortUrl}</a>`;
                document.getElementById('urlResult').innerHTML = anchor;
                buttonIsHidden();
            } else {
                alert(json.errorMessage);
            }
        });
}

function execCopy(e) {
    e.preventDefault();
    var text = document.getElementById("urlResult").innerText; 
    navigator.clipboard.writeText(text);
    
}

function buttonIsHidden() {
    buttonCopy.style.display = 'block'
}

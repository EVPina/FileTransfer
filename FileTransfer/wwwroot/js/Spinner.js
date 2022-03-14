

function MostrarSpinner() {
    const divcontent = document.createElement('div');
    const spinner = document.createElement('div');
    const span = document.createElement('span');
    span.classList.add("visually-hidden")
    spinner.classList.add("spinner-border");
    spinner.setAttribute("role","status");
    spinner.appendChild(span);
    divcontent.appendChild(spinner);
    divcontent.classList.add("ContentSpinner")
    return divcontent;
}

export { MostrarSpinner }
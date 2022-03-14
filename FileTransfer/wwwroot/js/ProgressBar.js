

function MostrarProgress(progreso) {
    let divcontent = document.createElement('div');
    let divline = document.createElement('div');
    divline.setAttribute("id","progress-bar")
    divline.classList.add("progress-bar")
    divline.setAttribute("role", "progressbar")
    divline.setAttribute("aria-valuenow", progreso)
    divline.setAttribute("aria-valuemin", progreso)
    divline.setAttribute("aria-valuemax", "1000")
    divcontent.classList.add("progress");
    divcontent.appendChild(divline);

    return divcontent;
}

export { MostrarProgress }
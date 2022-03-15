import { MostrarSpinner } from "./Spinner.js"

function BlockPage(elemento) {
    let spinner = MostrarSpinner()
    const div = document.createElement("div");
    div.setAttribute("id","WindowLoad")
    div.style.width = 100 + "%";
    div.style.height = 100 + "%";
    div.classList.add("WindowLoad");
    div.textContent = "Página Cargando"
    div.appendChild(spinner)
    elemento.insertBefore(div, elemento.firstChild);

    return div;
}

export { BlockPage }
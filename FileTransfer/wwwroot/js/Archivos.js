import { MostrarAlerta } from "./Alertas.js"
import { BlockPage } from "./BlockPage.js"
import { myModal, ModalText } from "./Modal.js"
const iduser = localStorage.getItem('iduser')
const ContentFiles = document.getElementById("ContentFiles")
const uploadarchivos = document.getElementById("uploadarchivos")
const ListArchivos = document.getElementById("ListArchivos")
const sendbutton = document.getElementById("Sendbutton")
const lista = ListArchivos.children[1]
let contador = 0
let files = new FormData();

//Cargar Archivos
uploadarchivos.addEventListener("change", function () {
    ListArchivos.style.display = "block";
    const allarchivos = uploadarchivos.files
    let cantidadarchivos = allarchivos.length
    if (cantidadarchivos + contador <= 10) {
        for (let i = 0; i < cantidadarchivos; i++) {
            let actualfile = uploadarchivos.files[i];
            let newfilename = actualfile.name.replace("/^\s+|\s+$/gm", "")
            let newfilename2 = newfilename.replace(/\s/g, "-")
            if (newfilename2.includes("#")) {
                ModalText("Advertencia", "El archivo no puede contener #")
                myModal.show();
            } else {
                files.append(newfilename2, actualfile)
                CrearItem(i, newfilename2, actualfile.size);
            }
        }
    }
    else {
        MostrarAlerta("Se excedio en el límite", "danger");
    }
})

 function SizeSuffix(bytes)
{
     const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB']
     if (bytes === 0) return 'n/a'
     const i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)), 10)
     if (i === 0) return `${bytes} ${sizes[i]})`
     return `${(bytes / (1024 ** i)).toFixed(1)} ${sizes[i]}`
}


function CrearItem(i, nombre_archivo, size_archivo) {
    contador++
    let li = document.createElement('li');
    let img = document.createElement("img");
    let spannamefile = document.createElement("span");
    let spansize = document.createElement("span");
    const sizetotal = SizeSuffix(size_archivo);
    lista.appendChild(li)
    img.src = document.URL.replace("Home/Archivos", "images/carpeta.png")
    li.appendChild(img)
    li.appendChild(spannamefile)
    spannamefile.textContent = nombre_archivo
    li.appendChild(spansize)
    spansize.textContent = sizetotal
    let button = document.createElement('button')
    button.classList.add("btn")
    button.classList.add("btn-danger")
    button.classList.add("boton_"+nombre_archivo)
    button.setAttribute("id", "DeleteButton")
    button.textContent = "Borrar"
    li.appendChild(button)
    button.addEventListener('click', function () {
        files.delete(nombre_archivo)
        button.parentElement.remove()
        contador--

    })
}

//Enviar Archivos
sendbutton.addEventListener("click", function (evt) {
    if (iduser === null | iduser === undefined) {
        location.replace("https://localhost:44311/Account/Login");
    } else {
        if (lista.children.length !== 0) {
            myModal.toggle()
            const blockpage = BlockPage(ContentFiles);
            $.ajax({
                type: 'post',
                contentType: false,
                processData: false,
                async: true,
                data: files,
                url: 'MoverArchivos?iduser=' + iduser,
                success: function (data) {
                    lista.textContent = "";
                    if (data.success) {
                        MostrarAlerta("Se guardo exitosamente", "success");
                    } else {
                        MostrarAlerta("Hubo un error", "danger");
                    }
                    contador = 0
                    let retrieveallfiles = data.files;
                    if (retrieveallfiles.length != 0) {
                        for (let indice = 0; indice < retrieveallfiles.length; indice++) {
                            CrearItem(indice, retrieveallfiles[indice].fileName, retrieveallfiles[indice].length);
                        }
                    } else {
                        for (var [key, value] of files.entries()) {
                            files.delete(key);
                        }
                    }
                    blockpage.remove();
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } else {
            console.log("No hay archivos")
        }
    }
})
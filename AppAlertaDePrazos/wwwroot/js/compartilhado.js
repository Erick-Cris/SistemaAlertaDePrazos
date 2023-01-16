function OpenModal(titulo, texto, endereco = null) {

    var myModal = new bootstrap.Modal(document.getElementById("exampleModal"), {});

    $("#exampleModalLabel").html(titulo);
    $("#modalbody").html(texto);

    myModal.show();

    
}
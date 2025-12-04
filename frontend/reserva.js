var formdata = {};

//abrir modal
document.querySelectorAll('.btn-abrirmodal').forEach(botao => {
    botao.addEventListener('click', () => {
        const modal = document.getElementById('modal-reserva');
        modal.style.display = "flex";

        //get de informações 
        getUnities();
    });
});


//fechar modal
document.getElementById('modal-close').addEventListener('click', () => {
    const modal = document.getElementById('modal-reserva');
    modal.style.display = "none";
});

async function getUnities() {
    console.log("Obtendo unidades..."); 
    const response = await fetch('http://localhost:5174/unity/list-with-rooms', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'  
        }
    })

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    const formatedResponse = await response.json();
    console.log('get realizado com sucesso:', formatedResponse)
    formdata = formatedResponse.data;

    const form = document.getElementById('modal-form');
    form.style.display = 'block';
    const loading = document.getElementById('modal-loading');
    loading.style.display = 'none';
    populateForm(formatedResponse.data);
}

function populateForm(data) {
    const inputUnidade = document.getElementById('input-unidade');
    data.forEach(unity => {
        const option = document.createElement('option');
        option.value = unity.id;
        option.textContent = unity.name;
        inputUnidade.appendChild(option);
    });
}

// seleção de sala
const selectUnity = document.getElementById("input-unidade");

selectUnity.addEventListener("change", function() {
    const valorSelecionado = this.value; // pega o valor
    const textoSelecionado = this.options[this.selectedIndex].text; // pega o texto visível

    console.log("Valor:", valorSelecionado);
    console.log("Texto:", textoSelecionado);

    var rooms = formdata.find(unity => unity.id == valorSelecionado).rooms;
    console.log(rooms); 

    const inputRoom = document.getElementById('input-sala');
    inputRoom.disabled = false;

    rooms.forEach(room => {
        const option = document.createElement('option');
        option.value = room.id;
        option.textContent = room.name;
        inputRoom.appendChild(option);
    });
});


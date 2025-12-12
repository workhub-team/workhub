// ----------------------------
// CHECAGEM DE LOGIN
// ----------------------------
//verifica se o usuario é admin e ta logado
window.addEventListener('load', function() {
    const userRole = localStorage.getItem("userRole");
    const token = localStorage.getItem("token");

    if (!token || !tokenValidate(token) || userRole !== 'admin') {
        window.location.href = 'index.html';
        return;
    }
});

function tokenValidate(token){
    //check token expiration 
    tokenPayload = JSON.parse(atob(token.split('.')[1]));
    const currentTime = Math.floor(Date.now() / 1000);
    const tokenexp = tokenPayload.exp;
    console.log("Token expira em:", new Date(tokenexp * 1000));
    return tokenexp > currentTime;
}

// ----------------------------
// UNIDADES
// ----------------------------
const formUnidade = document.getElementById("form-unidade");
const listaUnidades = document.getElementById("lista-unidades");
let unidades = [];

// listar
async function getUnidades() {
    const response = await fetch(`http://localhost:5174/unity/list`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}` 
        }
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    const formatedResponse = await response.json();
    console.log('get realizado com sucesso:', formatedResponse)

    return formatedResponse.data;
}

function setUnidades(unidades) {
    localStorage.setItem("unidades", JSON.stringify(unidades));
}

async function atualizarUnidades() {
    listaUnidades.innerHTML = "";
    const select = document.getElementById("filtro-unidade");
    unidades = await getUnidades();
    console.log(unidades);
    unidades.forEach(unidade => {
        const row = document.createElement('tr');   
        row.innerHTML = `
            <td>${unidade.name}</td>
            <td>${unidade.address}</td>
            <td>
                <button title="Editar registro" class="btn btn-editar" onclick="editarUnidade('${unidade.id}')">
                    <img src="img/edit.svg">
                </button>
                <button title="Excluir registro" class="btn btn-excluir" onclick="removerUnidade('${unidade.id}')">
                    <img src="img/delete.svg">
                </button>
            </td>
        `;
        listaUnidades.appendChild(row);

        const inputUnidade = document.getElementById('filtro-unidade');
        const option = document.createElement('option');
        option.value = unidade.id;
        option.textContent = unidade.name + " - " + unidade.address;
        inputUnidade.appendChild(option);
    });
}

// deletar 
async function removerUnidade(id) {
    console.log("Remover unidade de index:", id);

    const response = await fetch(`http://localhost:5174/unity/delete/`+id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}` 
        }
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    location.reload();
}

//editar 
async function editarUnidade(id) {
    unidade = unidades.find(u => u.id === id);
    const modalEditarUnidade = document.getElementById("modal-editar-unidade");
    modalEditarUnidade.style.display = "flex";
    document.getElementById("editar-nome-unidade").value = unidade.name;
    document.getElementById("editar-endereco-unidade").value = unidade.address;
    document.getElementById("editar-id-unidade").value = unidade.id;
}

async function updateUnidade(e) {
    const id = document.getElementById("editar-id-unidade").value.trim();
    const nome = document.getElementById("editar-nome-unidade").value.trim();
    const endereco = document.getElementById("editar-endereco-unidade").value.trim();
    if (!nome || !endereco) return;
    console.log(id, nome, endereco);

    const response = await fetch(`http://localhost:5174/unity/update`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify({
            id: id,
            name: nome,
            address: endereco
        })
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    console.log(response);
    location.reload();
}


const formUnidadeUpdate = document.getElementById('form-editar-unidade');
formUnidadeUpdate.addEventListener('submit', (e) => {
    e.preventDefault();
});

//fechar modal editar unidade
document.getElementById("fechar-modal-editar-unidade").addEventListener("click", () => {
    document.getElementById("modal-editar-unidade").style.display = "none";
});


// criar unidade

formUnidade.addEventListener("submit", e => {
    e.preventDefault();
    const nome = document.getElementById("nome-unidade").value.trim();
    const endereco = document.getElementById("endereco-unidade").value.trim();
    if (!nome || !endereco) return;

    createUnidade(nome, endereco);
});

async function createUnidade(nome, endereco) {
    const response = await fetch(`http://localhost:5174/unity/create`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify({
            name: nome,
            address: endereco
        })
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    console.log(response);
    location.reload();
}

// ----------------------------
// SALAS
// ----------------------------
const formSala = document.getElementById("form-sala");
const listaSalas = document.getElementById("lista-salas");
let unidadeSelecionada = "";
let unidadeSelecionadaNome = "";
let salas = [];

//atualiza unidade selecionada
document.getElementById("filtro-unidade").addEventListener("change", (e) => {
    unidadeSelecionada = e.target.value;
    unidadeSelecionadaNome = e.target.options[e.target.selectedIndex].text;
    console.log("Unidade selecionada:", unidadeSelecionadaNome, unidadeSelecionada);
    atualizarTabelaSalas();
});

async function atualizarTabelaSalas() {
    listaSalas.innerHTML = "";
    if (!unidadeSelecionada) return;
    const hiddenContent = document.getElementById("hidden");
    hiddenContent.style.display = "block";
    salas = await getSalas(unidadeSelecionada);
    salas.forEach((sala, index) => {
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${sala.name}</td>
            <td>${sala.seats}</td>
            <td>${sala.isShared? "Compartilhada" : "Privada"}</td>
            <td>
                <button title="Editar registro" class="btn btn-editar" onclick="editarSala('${sala.id}')">
                    <img src="img/edit.svg">
                </button>
                <button title="Excluir registro" class="btn btn-excluir" onclick="removerSala('${sala.id}')">
                    <img src="img/delete.svg">
                </button>
            </td>
        `;
        listaSalas.appendChild(tr);
    });
}

async function getSalas(unidadeId) {
    const response = await fetch(`http://localhost:5174/room/list/`+unidadeId, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}` 
        }
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    const formatedResponse = await response.json();
    console.log('get realizado com sucesso:', formatedResponse)

    return formatedResponse.data;
}

async function removerSala(index) {
    console.log("Remover sala de index:", id);

    const response = await fetch(`http://localhost:5174/room/delete/`+id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}` 
        }
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    location.reload();
}

//editar
function editarSala(id) {
    sala = salas.find(s => s.id === id);
    console.log(sala);

    const modalEditarSala = document.getElementById("modal-editar-sala");
    modalEditarSala.style.display = "flex";
    document.getElementById("editar-id-unidade-sala").value = unidadeSelecionada;
    document.getElementById("editar-id-sala").value = sala.id;
    document.getElementById("editar-nome-unidade-sala").value = unidadeSelecionadaNome;
    document.getElementById("editar-nome-sala").value = sala.name;
    document.getElementById("editar-capacidade-sala").value = sala.seats;
    
    if (sala.isShared) {
        document.getElementById("editar-tipo-sala").value = "true";
    } else {
        document.getElementById("editar-tipo-sala").value = "false";
    }
}

async function updateSala(e) {
    const idUnidade = document.getElementById("editar-id-unidade-sala").value.trim();
    const idSala = document.getElementById("editar-id-sala").value.trim();
    const nome = document.getElementById("editar-nome-sala").value.trim();
    const capacidade = document.getElementById("editar-capacidade-sala").value.trim();
    const compartilhada = document.getElementById("editar-tipo-sala").value.trim() === "true";

    if (!idUnidade || !idSala || !nome || !capacidade) return;
    console.log(idUnidade, idSala, nome, capacidade, compartilhada);

    const response = await fetch(`http://localhost:5174/room/update`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify({
            unity_id: idUnidade,
            room_id: idSala,
            name: nome,
            seats: parseInt(capacidade),
            is_shared: compartilhada
        })
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    console.log(response);

    document.getElementById("modal-editar-sala").style.display = "none";
    atualizarTabelaSalas();
}

//fechar modal editar unidade
document.getElementById("fechar-modal-editar-sala").addEventListener("click", () => {
    document.getElementById("modal-editar-sala").style.display = "none";
});

// criar sala
formSala.addEventListener("submit", e => {
    e.preventDefault();

    const nome = document.getElementById("nome-sala").value.trim();
    const capacidade = document.getElementById("capacidade-sala").value.trim();
    const compartilhada = document.getElementById("tipo-sala").value.trim() === "true";

    console.log("Criar sala na unidade:", nome, capacidade, compartilhada, unidadeSelecionada);  
    if (!nome || !capacidade) return;

    createSala(nome, capacidade, compartilhada, unidadeSelecionada);
});

async function createSala(nome, capacidade, compartilhada, unidadeSelecionada) {
    const response = await fetch(`http://localhost:5174/room/create`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify({
            unity_id: unidadeSelecionada,
            name: nome,
            seats: parseInt(capacidade),
            is_shared: compartilhada
        })
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    formSala.reset();
    atualizarTabelaSalas();
}

// deletar 
async function removerSala(id) {
    console.log("Remover sala de index:", id);

    const response = await fetch(`http://localhost:5174/room/delete/`+id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}` 
        }
    });

    if (!response.ok) {
        throw new Error(`Erro: ${response.status}`);
    }

    atualizarTabelaSalas();
}

// ----------------------------
// INICIALIZAÇÃO
// ----------------------------
atualizarTabelaSalas();
atualizarUnidades();

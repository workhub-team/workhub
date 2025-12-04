var formdata = {};

//define a data minima como hoje
const inputData = document.getElementById('input-data');

const hoje = new Date().toISOString().split('T')[0];

const limite = new Date();
limite.setDate(limite.getDate() + 30);
const limiteFormatado = limite.toISOString().split('T')[0];

inputData.setAttribute('min', hoje);
inputData.setAttribute("max", limiteFormatado);

//abrir modal
document.querySelectorAll('.btn-abrirmodal').forEach(botao => {
    botao.addEventListener('click', () => {
        //checar se o usuario ta logado
        const token = localStorage.getItem("token");
        if (token) {
            if (tokenValidate(token)) {
                console.log("User is logged in.");
                const modal = document.getElementById('modal-reserva');
                modal.style.display = "flex";

                //get de informações 
                getUnities();
            }
            else {
                localStorage.removeItem("token");
                console.log("Token is old. User is not logged in.");
                cleanUserData();
                irParaLogin();
            }
        }
        else {
            console.log("User is not logged in.");
            cleanUserData();
            irParaLogin();
        }

        
    });
});

function irParaLogin() {
    //mostrar alerta 
    const alertaElement = document.getElementById("aviso");
    alertaElement.style.display = "block"
    document.getElementById("reserva").scrollIntoView({
      behavior: "smooth", // rolagem suave
      block: "start"      // alinha no topo da tela
    });
}

//fechar modal
document.getElementById('modal-close').addEventListener('click', () => {
    //resetar form
    var form = document.getElementById('modal-form');
    form.reset();
    var inputUnidade = document.getElementById('input-unidade');
    inputUnidade.options.length = 1;
    var inputRoom = document.getElementById('input-sala');
    inputRoom.options.length = 1;
    inputRoom.disabled = true;
    var inputData = document.getElementById('input-data');
    inputData.disabled = true;
    var inputHora = document.getElementById('input-hora');
    inputHora.disabled = true;
    const modal = document.getElementById('modal-reserva');
    modal.style.display = "none";
    const pagamento = document.getElementById('modal-pagamento');
    pagamento.style.display = 'none';
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

    // const pagamento = document.getElementById('modal-pagamento');
    // pagamento.style.display = 'block';

    const loading = document.getElementById('modal-loading');
    loading.style.display = 'none';
    populateForm(formatedResponse.data);
}

function populateForm(data) {
    const inputUnidade = document.getElementById('input-unidade');
    //reset options
    
    data.forEach(unity => {
        const option = document.createElement('option');
        option.value = unity.id;
        option.textContent = unity.name + " - " + unity.address;
        inputUnidade.appendChild(option);
    });
}

// seleção de unidade
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
    inputRoom.options.length = 1;
    rooms.forEach(room => {
        const option = document.createElement('option');
        option.value = room.id;
        option.textContent = room.name;
        inputRoom.appendChild(option);
    });
});

//seleção de sala
const selectRoom = document.getElementById('input-sala');

selectRoom.addEventListener("change", function() {
    if (this.value != "") {
        //popula campo de capacidade
        foundCapacity = formdata.find(unity => unity.id == selectUnity.value)
            .rooms.find(room => room.id == this.value).seats;
        console.log("Capacidade da sala selecionada:", foundCapacity);
        const inputCapacidade = document.getElementById('input-capacidade');
        // inputCapacidade.disabled = false;
        inputCapacidade.value = foundCapacity + " Pessoas";
        
        //popula campo de tipo
        foundTipo = formdata.find(unity => unity.id == selectUnity.value)
            .rooms.find(room => room.id == this.value).isShared ? "Compartilhada" : "Privada";
        console.log("Tipo da sala selecionada:", foundTipo);
        const inputTipo = document.getElementById('input-tipo');
        inputTipo.value = foundTipo

        const inputData = document.getElementById('input-data');
        inputData.disabled = false;
    }
});

//seleção de turno
const inputDate = document.getElementById('input-data');

inputDate.addEventListener("change", function() {
    if (this.value != "") {
        const inputHora = document.getElementById('input-hora');
        inputHora.disabled = false;
    }  
});

//verificar disponibilidade
const btnVerificar = document.getElementById('verificar-disponibilidade');

btnVerificar.addEventListener("click", function() {
    tryVerify();
});

async function tryVerify(){
    const roomId = document.getElementById('input-sala').value;
    const userId = localStorage.getItem("userId");
    const reservedDay = document.getElementById('input-data').value;
    const reservedPeriod = document.getElementById('input-hora').value;
    const unidade = document.getElementById('input-unidade').value;

    const response = await fetch('http://localhost:5174/reserve/verify', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}` 
        },
        body: JSON.stringify({ 
            room_id: roomId,
            user_id: userId,
            reserved_day: reservedDay,
            reserved_period: reservedPeriod
        })
    })

    if (!response.ok) {
        if (response.status === 409) {
            alert("A sala já está reservada para o período selecionado.");
            return;
        }
        throw new Error(`Erro: ${response.status}`);
    }

    const formatedResponse = await response.json();
    console.log('Verificação realizada com sucesso:', formatedResponse)


    const form = document.getElementById('modal-form');
    form.style.display = 'none';
    const pagamento = document.getElementById('modal-pagamento');
    pagamento.style.display = 'block';

    //resumo reserva
    const unidadeElemento = document.getElementById('input-unidade');
    const unidadeTexto = unidadeElemento.selectedOptions[0].text;
    const resumoUnidade = document.getElementById("resumo-unidade");
    resumoUnidade.textContent = unidadeTexto;

    const salaElemento = document.getElementById('input-sala');
    const salaTexto = salaElemento.selectedOptions[0].text;
    const resumoSala = document.getElementById("resumo-sala");
    resumoSala.textContent = salaTexto;

    const resumoData = document.getElementById("resumo-data");
    resumoData.textContent = reservedDay;

    const resumoTurno = document.getElementById("resumo-turno");
    if (reservedPeriod == "full") resumoTurno.textContent = "Dia Inteiro";
    else resumoTurno.textContent = reservedPeriod;

    precoFinal = formatedResponse.data[0].price + ",00";
    const resumoPreco = document.getElementById("resumo-preco");
    resumoPreco.textContent = precoFinal;
}

//selecionar forma de pagamento
const radios = document.querySelectorAll('input.forma-pagamento[type="radio"]');
radios.forEach(r => r.addEventListener('change', onFormaPagamentoChange));
function onFormaPagamentoChange(event) {
    const radio = event.target;
    if (!radio.checked) return; 

    //desbloqueia botao de confirmação
    

    const valor = radio.value; // ex.: "pix", "cartao", "boleto"
    console.log('Forma de pagamento:', valor);

    //desbloqueia
}

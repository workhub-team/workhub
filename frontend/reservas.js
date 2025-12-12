//verifica se o usuario é admin e ta logado
window.addEventListener('load', function() {
    const token = localStorage.getItem("token");

    if (!tokenValidate(token)) {
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

loadReserves();

async function loadReserves() {
    const user_id = localStorage.getItem("userId");
    const response = await fetch(`http://localhost:5174/reserve/list/${user_id}`, {
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
    formdata = formatedResponse.data;
    console.log(formdata);

    renderTable(formdata);
}

function renderTable(data) {
    const tableBody = document.getElementById('corpo-tabela-reservas');
    tableBody.innerHTML = '';
    data.forEach(reserve => {
        const row = document.createElement('tr');   
        row.innerHTML = `
            <td>${new Date(reserve.reserve_date).toLocaleDateString('pt-BR')}</td>
            <td>${reserve.reserve_period == "full"? "Dia Completo": capitalizar(reserve.reserve_period)}</td>
            <td>${reserve.room_name}</td>
            <td>${reserve.unity_name}</td>
            <td>${reserve.access_code}</td>
        `;
        tableBody.appendChild(row);
    });
}


function capitalizar(palavra) {
  return palavra.charAt(0).toUpperCase() + palavra.slice(1).toLowerCase();
}

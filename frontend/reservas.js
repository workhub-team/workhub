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
            <td>${new Date(reserve.reservedDay).toLocaleDateString('pt-BR')}</td>
            <td>${reserve.reservedPeriod == "full"? "Dia Completo": capitalizar(reserve.reservedPeriod)}</td>
            <td>${reserve.roomId}</td>
            <td>${reserve.accessCode}</td>
        `;
        tableBody.appendChild(row);
    });
}


function capitalizar(palavra) {
  return palavra.charAt(0).toUpperCase() + palavra.slice(1).toLowerCase();
}

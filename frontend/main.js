checkToken();

// checagem de token
function checkToken() {
    // search for token in local storage
    const token = localStorage.getItem("token");
    if (token) {
        if (tokenValidate(token)) {
            console.log("User is logged in.");
            hideLogin();
        }
        else {
            localStorage.removeItem("token");
            console.log("Token is old. User is not logged in.");
            cleanUserData();
        }
    }
    else {
        console.log("User is not logged in.");
        cleanUserData();
    }
}

function hideLogin() {
    const sectionReserva = document.getElementById('reserva');
    sectionReserva.style.display = "none"
    const botaoLogin = document.getElementById('entrar');
    botaoLogin.style.display = "none"

    //mostrar botao de sair
    const botaoSair = document.getElementById('sair');
    botaoSair.style.display = "flex"
}

function cleanUserData() {
    localStorage.removeItem("token");
    localStorage.removeItem("userRole")
    localStorage.removeItem("userId");
    localStorage.removeItem("userName");
}

function tokenValidate(token) {
    //check token expiration 
    tokenPayload = JSON.parse(atob(token.split('.')[1]));
    const currentTime = Math.floor(Date.now() / 1000);
    const tokenexp = tokenPayload.exp;
    console.log("Token expira em:", new Date(tokenexp * 1000));
    return tokenexp > currentTime;
}

// login
document.getElementById('btn-login').addEventListener('click', function () {
    const email = document.getElementById('login-email').value.trim();
    const senha = document.getElementById('login-senha').value.trim();
    if (!email || !senha) {
        showToast('Preencha e-mail e senha para entrar.', 'warning');
        return;
    }
    tryLogin(email, senha);
});

async function tryLogin(email, senha) {
    const response = await fetch('http://localhost:5174/auth/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email: email,
            password: senha
        })
    })

    if (!response.ok) {
        if (response.status === 404) {
            console.error("Usuário não foi encontrado")
            const notFound = document.getElementById('nao-encontrado')
            notFound.style.display = "block"
            return;
        }

        throw new Error(`Erro: ${response.status}`);
    }

    const formatedResponse = await response.json();
    console.log('Login realizado com sucesso:', formatedResponse)

    localStorage.setItem("token", formatedResponse.jwt_token);
    localStorage.setItem("userRole", formatedResponse.user_role);
    localStorage.setItem("userId", formatedResponse.user_id);
    localStorage.setItem("userName", formatedResponse.user_name);

    // Recarrega a página
    history.scrollRestoration = "manual";
    location.reload();

}

//deslogin manual
document.getElementById('sair').addEventListener('click', function () {
    cleanUserData();
    // Recarrega a página
    location.reload();
});

function irParaLogin2() {
    document.getElementById("reserva").scrollIntoView({
        behavior: "smooth", // rolagem suave
        block: "start"      // alinha no topo da tela
    });
}

// cadastro
document.getElementById('btn-criar').addEventListener('click', function () {
    const nome = document.getElementById('nome').value.trim();
    const email = document.getElementById('email').value.trim();
    const senha = document.getElementById('senha').value.trim();
    if (!nome || !email || !senha) {
        showToast('Por favor preencha todos os campos para criar a conta.', 'warning');
        return;
    }
    // simula criação e login
    trySubscribe(nome, email, senha);
});

async function trySubscribe(nome, email, senha) {
    const formCriar = document.getElementById('form-criar');
    const formEntrar = document.getElementById('form-entrar');
    const aviso = document.getElementById('aviso');

    const response = await fetch('http://localhost:5174/auth/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            username: nome,
            password: senha,
            email: email,
        })
    })

    if (!response.ok) {
        console.error(`Erro: ${response.status}`);

        if (response.status === 409) {
            showToast('E-mail já cadastrado. Por favor, utilize outro e-mail.', 'error');
            return;
        }
    }

    const userid = await response.text();
    console.log('usuário cadastrado realizado com sucesso:', userid)

    // alert('Conta criada com sucesso! Você está logado.');

    //redirecionar para login
    formCriar.style.display = "none"
    aviso.style.display = "none"
    formEntrar.style.display = "flex"
}

// Toast Notification System
function showToast(message, type = 'info') {
    const container = document.getElementById('toast-container');
    const toast = document.createElement('div');
    toast.className = `toast ${type}`;

    let icon = 'ℹ️';
    if (type === 'success') icon = '✅';
    if (type === 'error') icon = '❌';
    if (type === 'warning') icon = '⚠️';

    toast.innerHTML = `
        <span class="toast-icon">${icon}</span>
        <span class="toast-message">${message}</span>
    `;

    container.appendChild(toast);

    // Auto remove after 3 seconds
    setTimeout(() => {
        toast.style.animation = 'slideOut 0.3s ease forwards';
        toast.addEventListener('animationend', () => {
            toast.remove();
        });
    }, 3000);
}

// Accessibility: Font Size & Family Control
document.addEventListener('DOMContentLoaded', () => {
    const btnIncrease = document.getElementById('btn-increase-font');
    const btnDecrease = document.getElementById('btn-decrease-font');
    const btnToggleFont = document.getElementById('btn-toggle-font');
    const root = document.documentElement;

    // Font Size Logic
    let currentFontSize = 100; // percentage
    const minFontSize = 80;
    const maxFontSize = 150;
    const step = 10;

    function updateFontSize() {
        root.style.fontSize = `${currentFontSize}%`;
        localStorage.setItem('userFontSize', currentFontSize);
    }

    const savedFontSize = localStorage.getItem('userFontSize');
    if (savedFontSize) {
        currentFontSize = parseInt(savedFontSize);
        updateFontSize();
    }

    if (btnIncrease) {
        btnIncrease.addEventListener('click', () => {
            if (currentFontSize < maxFontSize) {
                currentFontSize += step;
                updateFontSize();
            }
        });
    }

    if (btnDecrease) {
        btnDecrease.addEventListener('click', () => {
            if (currentFontSize > minFontSize) {
                currentFontSize -= step;
                updateFontSize();
            }
        });
    }

    // Font Family Logic
    const fonts = ['var(--font-sans)', 'var(--font-serif)', 'var(--font-mono)'];
    let currentFontIndex = 0;

    function updateFontFamily() {
        root.style.setProperty('--font-main', fonts[currentFontIndex]);
        localStorage.setItem('userFontIndex', currentFontIndex);
    }

    const savedFontIndex = localStorage.getItem('userFontIndex');
    if (savedFontIndex) {
        currentFontIndex = parseInt(savedFontIndex);
        updateFontFamily();
    }

    if (btnToggleFont) {
        btnToggleFont.addEventListener('click', () => {
            currentFontIndex = (currentFontIndex + 1) % fonts.length;
            updateFontFamily();
        });
    }
});
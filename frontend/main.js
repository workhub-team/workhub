checkToken();

function checkToken(){
    // search for token in local storage
    const token = localStorage.getItem("token");
    if (token) {
        if (tokenValidate(token)) {
            console.log("User is logged in.");
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

function cleanUserData(){
    localStorage.removeItem("token");
    localStorage.removeItem("userRole")
    localStorage.removeItem("userId");
    localStorage.removeItem("userName");
}

// login
document.getElementById('btn-login').addEventListener('click', function(){
    const email = document.getElementById('login-email').value.trim();
    const senha = document.getElementById('login-senha').value.trim();
    if(!email || !senha){
        alert('Preencha e-mail e senha para entrar.');
        return;
    }
    tryLogin(email, senha);
});

async function tryLogin(email, senha){
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
        throw new Error(`Erro: ${response.status}`);
    }

    const formatedResponse = await response.json();
    console.log('Login realizado com sucesso:', formatedResponse)

    localStorage.setItem("token", formatedResponse.jwt_token);
    localStorage.setItem("userRole", formatedResponse.user_role);
    localStorage.setItem("userId", formatedResponse.user_id);
    localStorage.setItem("userName", formatedResponse.user_name);
}

function tokenValidate(token){
    //check token expiration 
    tokenPayload = JSON.parse(atob(token.split('.')[1]));
    const currentTime = Math.floor(Date.now() / 1000);
    const tokenexp = tokenPayload.exp;
    console.log("Token expira em:", new Date(tokenexp * 1000));
    return tokenexp > currentTime;
}
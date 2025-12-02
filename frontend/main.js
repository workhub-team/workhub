

var isloggedIn = false;

// search for token in local storage
const token = localStorage.getItem("token");
if (token) {
    isloggedIn = true;
    console.log("User is logged in.");
}
else console.log("User is not logged in.");

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

function tryLogin(email, senha){
    fetch('https://workhub-backend.onrender.com/auth/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'  
        },
        body: JSON.stringify({ email, senha })
    })
}
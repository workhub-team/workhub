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
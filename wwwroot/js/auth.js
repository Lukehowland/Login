// Aquí debes asegurarte de que firebase esté cargado antes de usar firebase.auth()
const auth = firebase.auth();

document.getElementById('registerForm').addEventListener('submit', (e) => {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    auth.createUserWithEmailAndPassword(email, password)
        .then((userCredential) => {
            const user = userCredential.user;
            // Redirigir o mostrar mensaje de éxito
        })
        .catch((error) => {
            console.error('Error al registrar usuario', error);
        });
});

document.getElementById('googleRegister').addEventListener('click', () => {
    const provider = new firebase.auth.GoogleAuthProvider();

    auth.signInWithPopup(provider)
        .then((result) => {
            const user = result.user;
            // Redirigir o mostrar mensaje de éxito
        })
        .catch((error) => {
            console.error('Error al registrar usuario con Google', error);
        });
});


function cerrarSesion() {
    firebase.auth().signOut().then(() => {
        window.location.href = '/Account/Login';
    }).catch((error) => {
        console.error('Error al cerrar sesión:', error);
    });
}

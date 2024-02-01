async function login() {
    
    const Login = document.getElementById('Input_Index').value;
    const Password = document.getElementById('Input_Password').value;

    try {
        const response = await fetch('api/loginapi', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ Login, Password }),
        });

        if (response.ok) {
            data = response.text();
            localStorage.setItem('token', data);
            // Redirect or perform any other action after successful login
            console.log('Login successful');
            window.location.href = '/'
        } 
        else {
            console.error('Login failed');
        }
    } catch (error) {
        console.error('Error during login:', error);
    }
}

function checkAuthentication() {
    const token = localStorage.getItem('token');
    if (!token) {
        // Redirect to the login page or handle unauthorized access
        window.location.href = '/login';
        return 0;
    }
    return 1;
}



function logout() {
    // Clear the token from local storage
    localStorage.removeItem('token');

    // Redirect or perform any other action after logout
    window.location.href = '/login';
}
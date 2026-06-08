import http from 'k6/http';

export function getToken(baseUrl) {

    const res = http.post(
        `${baseUrl}/api/auth/login`,
        JSON.stringify({
            email: 'test@gmail.com',
            passwordHash: 'test',
        }),
        {
            headers: {
                'Content-Type': 'application/json'
            }
        }
    );

    if (res.status !== 200) {
        console.log(res.body);
        throw new Error('Login failed');
    }

    const token = res.json('token');

    return token;
}
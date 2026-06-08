import http from 'k6/http';
import { check } from 'k6';
import { getToken } from './helpers/auth.js'

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5000'; 

export const options = {
    vus: 1,
    duration: '10s',
}

export default function () {
    const token = getToken(BASE_URL);
    
    check(token, {
        'login success': (t) => t !== null,
    });
}
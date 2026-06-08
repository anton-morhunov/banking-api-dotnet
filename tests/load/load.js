import http from 'k6/http';
import { check, sleep } from 'k6';
import {getToken} from './helpers/auth.js';

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5000';

export const options = {
    stages:[
        {duration: '2m', target: 10},
        {duration: '5m', target: 10},
        {duration: '2m', target: 0},
    ],
    thresholds: {
        http_req_duration: ['p(95)<500'],
        http_req_failed: ['rate<0.01']
    },
};

export default function () {
    const token = getToken(BASE_URL);

    const headers = {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json'
    };

    const accounts = http.get(`${BASE_URL}/api/accounts`, {headers});
    check(accounts, {'accounts status: 200': (r) => r.status === 200});

    const clients = http.get(`${BASE_URL}/api/clients`, {headers});
    check(clients, {'clients status: 200': (r) => r.status === 200});

    sleep(1)
}

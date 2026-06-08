import http from 'k6/http';
import { check, sleep } from 'k6';
import { getToken } from './helpers/auth.js';

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5000';

export const options = {
    stages: [
        { duration: '1m', target: 50 },
        { duration: '2m', target: 50 },
        { duration: '1m', target: 100 },
        { duration: '2m', target: 100 },
        { duration: '1m', target: 200 },
        { duration: '2m', target: 200 },
        { duration: '1m', target: 0 },
    ],

    thresholds: {
        http_req_duration: ['p(95)<1000'],
        http_req_failed: ['rate<0.05'],
    },
};

const ACCOUNT_STATUSES = [0, 1, 2];

export function setup() {

    const token = getToken(BASE_URL);

    if (!token) {
        throw new Error('JWT token was not received');
    }

    const headers = {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json',
    };

    const clientsResponse = http.get(
        `${BASE_URL}/api/clients`,
        { headers }
    );

    logError(
        clientsResponse,
        'SETUP GET /clients',
        token
    );

    const accountsResponse = http.get(
        `${BASE_URL}/api/accounts`,
        { headers }
    );

    logError(
        accountsResponse,
        'SETUP GET /accounts',
        token
    );

    if (
        clientsResponse.status !== 200 ||
        accountsResponse.status !== 200
    ) {
        throw new Error(
            'Could not load clients/accounts in setup()'
        );
    }

    const clients = JSON.parse(clientsResponse.body);
    const accounts = JSON.parse(accountsResponse.body);

    return {
        token,
        clientIds: clients.map(c => c.id),
        accountIds: accounts.map(a => a.accountId ?? a.id)
    };
}

export default function (data) {

    const headers = {
        Authorization: `Bearer ${data.token}`,
        'Content-Type': 'application/json',
    };

    const clientId =
        data.clientIds[
            Math.floor(Math.random() * data.clientIds.length)
            ];

    const accountId =
        data.accountIds[
            Math.floor(Math.random() * data.accountIds.length)
            ];

    const randomAccountStatus =
        ACCOUNT_STATUSES[
            Math.floor(Math.random() * ACCOUNT_STATUSES.length)
            ];

    const uniqueId =
        `${__VU}-${__ITER}-${Date.now()}`;

    const clientPayload = JSON.stringify({
        name: `StressUser_${uniqueId}`,
        email: `stress_${uniqueId}@gmail.com`,
        phoneNumber:
            `+81${Math.floor(
                1000000000 +
                Math.random() * 9000000000
            )}`
    });

    const accountPayload = JSON.stringify({
        clientId,
        accountType: Math.floor(Math.random() * 2)
    });

    const patchPayload = JSON.stringify({
        status: randomAccountStatus
    });

    const rnd = Math.random();

    // 70% READ
    if (rnd < 0.7) {

        const accounts = http.get(
            `${BASE_URL}/api/accounts`,
            {
                headers,
                tags: {
                    endpoint: 'GET /accounts'
                }
            }
        );

        check(accounts, {
            'accounts status 200':
                r => r.status === 200
        });

        logError(
            accounts,
            'GET /accounts',
            data.token
        );

        const account = http.get(
            `${BASE_URL}/api/accounts/${accountId}`,
            {
                headers,
                tags: {
                    endpoint: 'GET /accounts/{id}'
                }
            }
        );

        check(account, {
            'account was found':
                r => r.status === 200
        });

        logError(
            account,
            `GET /accounts/${accountId}`,
            data.token
        );

        const clients = http.get(
            `${BASE_URL}/api/clients`,
            {
                headers,
                tags: {
                    endpoint: 'GET /clients'
                }
            }
        );

        check(clients, {
            'clients status 200':
                r => r.status === 200
        });

        logError(
            clients,
            'GET /clients',
            data.token
        );

        const client = http.get(
            `${BASE_URL}/api/clients/${clientId}`,
            {
                headers,
                tags: {
                    endpoint: 'GET /clients/{id}'
                }
            }
        );

        check(client, {
            'client was found':
                r => r.status === 200
        });

        logError(
            client,
            `GET /clients/${clientId}`,
            data.token
        );
    }

    // 20% CREATE
    else if (rnd < 0.9) {

        const postClient = http.post(
            `${BASE_URL}/api/clients`,
            clientPayload,
            {
                headers,
                tags: {
                    endpoint: 'POST /clients'
                }
            }
        );

        check(postClient, {
            'client created':
                r => r.status === 200 ||
                    r.status === 201
        });

        logError(
            postClient,
            'POST /clients',
            data.token
        );

        const postAccount = http.post(
            `${BASE_URL}/api/accounts`,
            accountPayload,
            {
                headers,
                tags: {
                    endpoint: 'POST /accounts'
                }
            }
        );

        check(postAccount, {
            'account created':
                r => r.status === 200 ||
                    r.status === 201
        });

        logError(
            postAccount,
            'POST /accounts',
            data.token
        );
    }

    // 10% UPDATE
    else {

        const patchAccount = http.patch(
            `${BASE_URL}/api/accounts/${accountId}/status`,
            patchPayload,
            {
                headers,
                tags: {
                    endpoint:
                        'PATCH /accounts/{id}/status'
                }
            }
        );

        check(patchAccount, {
            'account status changed':
                r => r.status === 200
        });

        logError(
            patchAccount,
            `PATCH /accounts/${accountId}/status`,
            data.token
        );
    }

    sleep(1);
}